//referenced https://coolaj86.com/articles/webcrypto-encrypt-and-decrypt-with-aes/ & 
// https://medium.com/perimeterx/fun-times-with-webcrypto-part-2-encrypting-decrypting-dfb9fadba5bc

const utf8Encoder = new TextEncoder("utf-8");
const utf8decoder = new TextDecoder("utf-8");
const crypto = window.crypto;
const ivLen = 16; // the IV is always 16 bytes
const aesName = "AES-CBC";
let rawKey;
let encryptedMessage;

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
        then(function (passphraseKey) {
            return crypto.subtle.deriveKey(
                {
                    "name": kdfName
                    , "salt": saltBuf
                    , "iterations": iterations
                    , "hash": hashName
                }
                , passphraseKey
                , { "name": aesName, "length": keyLenBits } // Key we want
                , true                                      // Extractable
                , ["encrypt", "decrypt"]                    // For new key
            );
        }).
        then(function (aesKey) {
            return crypto.subtle.exportKey("raw", aesKey);
        }).
        catch(function (err) {
            console.log(`Key derivation failed: ${err.message}`);
        });
}


function joinIvAndData(iv, data) {
    let buf = new Uint8Array(iv.length + data.length);
    Array.prototype.forEach.call(iv, function (byte, i) {
        buf[i] = byte;
    });
    Array.prototype.forEach.call(data, function (byte, i) {
        buf[ivLen + i] = byte;
    });
    return buf;
}

async function encrypt(data, key) {
    // a public value that should be generated for changes each time
    let initializationVector = new Uint8Array(ivLen);
    crypto.getRandomValues(initializationVector);

    return crypto.subtle.encrypt(
        { name: aesName, iv: initializationVector }
        , key
        , data
    ).then(function (encrypted) {
        return joinIvAndData(initializationVector, new Uint8Array(encrypted));
    });
}

function separateIvFromData(buf) {
    let iv = new Uint8Array(ivLen);
    let data = new Uint8Array(buf.length - ivLen);

    Array.prototype.forEach.call(buf, function (byte, i) {
        if (i < ivLen) {
            iv[i] = byte;
        } else {
            data[i - ivLen] = byte;
        }
    });
    return { iv: iv, data: data };
}

function decrypt(buf, key) {
    const parts = separateIvFromData(buf);

    return crypto.subtle.decrypt(
        { name: aesName, iv: parts.iv }
        , key
        , parts.data
    ).then(function (decrypted) {
        return decrypted;
    });
}

async function test() {
    await generateKey();
    const message = await encryptText("privasight");
    console.log(message);
    const decryptedBuff = await decryptText(message);
    console.log(utf8decoder.decode(decryptedBuff));
}

async function generateKey() {
    //todo save the saltBuf to indexedDb
    let saltBuf = new Uint8Array(ivLen);
    crypto.getRandomValues(saltBuf);
    //todo check if it is 16 characters long
    const passphrase = prompt("Enter Password");

    await deriveKey(saltBuf, passphrase).then(function (aesKey) {
        rawKey = aesKey;
    });
}

async function encryptText(text) {
    const key = await crypto.subtle.importKey("raw", rawKey, aesName, true, ["encrypt"]);

    const encryptStr = await encrypt(utf8Encoder.encode(text), key);
    return encryptStr;
}

async function decryptText(text) {
    const key = await crypto.subtle.importKey("raw", rawKey, aesName, true, ["decrypt"]);
    const decryptStr = await decrypt(text, key);
    return decryptStr;
}