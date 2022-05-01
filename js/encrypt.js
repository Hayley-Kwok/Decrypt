//referenced https://coolaj86.com/articles/webcrypto-encrypt-and-decrypt-with-aes/ &
// https://medium.com/perimeterx/fun-times-with-webcrypto-part-2-encrypting-decrypting-dfb9fadba5bc

const idbPromise = import("https://cdn.jsdelivr.net/npm/idb-keyval@6/+esm");
const utf8Encoder = new TextEncoder("utf-8");
const utf8decoder = new TextDecoder("utf-8");

const crypto = window.crypto;
const ivLen = 16; // the IV is always 16 bytes
const aesName = "AES-CBC";

const companyDataKey = "companyData";
const dashboardSettingsKey = "dashboardSettings";

let pwTriedTimes = 0;
let rawKey;
let encryptedMessage;
let idb;

function deriveKey(saltBuf, passphrase) {
    const keyLenBits = 128;
    const kdfName = "PBKDF2";
    const iterations = 1000;
    const hashName = "SHA-256";

    // First, create a PBKDF2 "key" containing the password
    return crypto.subtle.importKey(
            "raw",
            utf8Encoder.encode(passphrase),
            { "name": kdfName },
            false,
            ["deriveKey"]).
        // Derive a key from the password
        then(function(passphraseKey) {
            return crypto.subtle.deriveKey(
                {
                    "name": kdfName,
                    "salt": saltBuf,
                    "iterations": iterations,
                    "hash": hashName
                },
                passphraseKey,
                { "name": aesName, "length": keyLenBits } // Key we want
                ,
                true // Extractable
                ,
                ["encrypt", "decrypt"] // For new key
            );
        }).then(function(aesKey) {
            return crypto.subtle.exportKey("raw", aesKey);
        }).catch(function(err) {
            console.log(`Key derivation failed: ${err.message}`);
        });
}


function joinIvAndData(iv, data) {
    let buf = new Uint8Array(iv.length + data.length);
    Array.prototype.forEach.call(iv,
        function(byte, i) {
            buf[i] = byte;
        });
    Array.prototype.forEach.call(data,
        function(byte, i) {
            buf[ivLen + i] = byte;
        });
    return buf;
}

async function encrypt(data, key) {
    // a public value that should be generated for changes each time
    let initializationVector = new Uint8Array(ivLen);
    crypto.getRandomValues(initializationVector);

    return crypto.subtle.encrypt(
        { name: aesName, iv: initializationVector },
        key,
        data
    ).then(function(encrypted) {
        return joinIvAndData(initializationVector, new Uint8Array(encrypted));
    });
}

function separateIvFromData(buf) {
    let iv = new Uint8Array(ivLen);
    let data = new Uint8Array(buf.length - ivLen);

    Array.prototype.forEach.call(buf,
        function(byte, i) {
            if (i < ivLen) {
                iv[i] = byte;
            } else {
                data[i - ivLen] = byte;
            }
        });
    return { iv: iv, data: data };
}

async function decrypt(buf, key) {
    const parts = separateIvFromData(buf);

    try {
        return await crypto.subtle.decrypt(
            { name: aesName, iv: parts.iv },
            key,
            parts.data
        );
    } catch (e) {
        throw e;
    }
}

async function getIdb() {
    if (idb === undefined) {
        idb = await idbPromise;
    }
}

async function generateKey() {
    if (rawKey !== undefined) {
        return true;
    }

    await getIdb();
    let newPassword = false;
    const saltFromStorage = await idb.get("salt");
    if (saltFromStorage === undefined) {
        newPassword = true;
    }

    let passphrase;

    const message = newPassword
        ? "Create Password(at least 16 characters long)"
        : "Enter your password (at least 16 characters long)";

    do {
        passphrase = prompt(message);
        pwTriedTimes += 1;
    } while
        (pwTriedTimes < 5 &&
        (passphrase === undefined || passphrase === null || passphrase === "" || passphrase.length < 16));
    if (pwTriedTimes >= 5) {
        window.alert("wrong password and ran out of attempts");
        return false;
    }

    let saltBuf;
    if (newPassword) {
        saltBuf = new Uint8Array(ivLen);
        crypto.getRandomValues(saltBuf);
        await idb.set("salt", saltBuf);
    } else {
        saltBuf = saltFromStorage;
    }

    await deriveKey(saltBuf, passphrase).then(function(aesKey) {
        rawKey = aesKey;
    });
    return true;
}

async function encryptText(text) {
    const key = await crypto.subtle.importKey("raw", rawKey, aesName, true, ["encrypt"]);

    const encryptStr = await encrypt(utf8Encoder.encode(text), key);
    return encryptStr;
}

async function decryptText(text) {
    const key = await crypto.subtle.importKey("raw", rawKey, aesName, true, ["decrypt"]);
    try {
        return await decrypt(text, key);
    } catch (e) {
        throw e;
    }
}


async function setCompanyData(companyData) {
    const keyGenerated = await generateKey();
    if (!keyGenerated) {
        return;
    }

    const encryptedBuff = await encryptText(companyData);

    await getIdb();

    await idb.set(companyDataKey, encryptedBuff);
}

async function getCompanyData() {
    const keyGenerated = await generateKey();
    if (!keyGenerated) {
        return "";
    }
    await getIdb();
    const encryptedBuff = await idb.get(companyDataKey);
    if (encryptedBuff === undefined) {
        return "";
    }
    try {
        const decryptedBuff = await decryptText(encryptedBuff);
        return utf8decoder.decode(decryptedBuff);
    } catch (e) {
        window.alert("Failed to decrypt data. It could be caused by the wrong password. " +
            "Please refresh and try again.");
        return "error";
    }
}

async function setDashboardSettings(dashBoardSettings) {
    await getIdb();

    await idb.set(dashboardSettingsKey, dashBoardSettings);
}

async function getDashboardSettings() {
    await getIdb();

    const dashboardSettings = await idb.get(dashboardSettingsKey);
    if (dashboardSettings === undefined) {
        return "";
    }
    return dashboardSettings;
}