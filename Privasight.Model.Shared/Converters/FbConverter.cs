﻿using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Privasight.Model.Shared.Converters
{
    /// <summary>
    /// There is an issue with the Data from Meta. It seems to be incorrectly encoded.
    /// So all string data from them have to go through this converter to be properly decoded.
    /// </summary>
    /// 
    /// <remarks>
    /// what happened https://stackoverflow.com/questions/50008296/facebook-json-badly-encoded
    /// solution referenced from https://stackoverflow.com/questions/50799187/encoding-decoding-issue-with-facebook-json-messages-c-sharp-parsing
    /// the same problem is also found in Instagram data
    /// </remarks>
    
    public class FbConverter : JsonConverter<string>
    {
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var latin1 = Encoding.GetEncoding("ISO-8859-1");

            var bytes = reader.ValueSpan.ToArray();
            var str = Encoding.UTF8.GetString(bytes);

            var unescapedStr = Regex.Unescape(str);
            return Encoding.UTF8.GetString(latin1.GetBytes(unescapedStr));
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}
