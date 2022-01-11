using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Decrypt.Model.Converters
{
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
