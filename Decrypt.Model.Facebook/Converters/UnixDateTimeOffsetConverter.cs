using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Decrypt.Model.Facebook.Converters
{
    //solution referenced from https://stackoverflow.com/a/63884990
    public class UnixDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTimeOffset.UnixEpoch.AddSeconds(reader.GetInt64());
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue((value - DateTime.UnixEpoch).TotalSeconds.ToString(CultureInfo.InvariantCulture));
        }
    }
}
