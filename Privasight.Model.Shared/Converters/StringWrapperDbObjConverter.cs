using Privasight.Model.Shared.DataStructures;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Privasight.Model.Shared.Converters
{
    /// <summary>
    /// Json Converter that turn a string in the json file to the StringWrapperDbObj
    /// </summary>
    public class StringWrapperDbObjConverter : JsonConverter<StringWrapperDbObj>
    {
        public override StringWrapperDbObj? Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            return reader.TokenType == JsonTokenType.String ? 
                new StringWrapperDbObj(reader.GetString() ?? string.Empty) : 
                JsonSerializer.Deserialize<StringWrapperDbObj>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, StringWrapperDbObj value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}