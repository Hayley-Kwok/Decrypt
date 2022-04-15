using System.Text.Json.Serialization;
using Privasight.Model.Shared;
using Privasight.Model.Shared.Interfaces;

namespace Privasight.Model.Facebook
{
    public class InferredTopics : ISingleItemListFile<StringWrapperDbObj>
    {
        public static readonly string Filepath = @"your_topics/your_topics.json";

        public string Title => "Inferred Topics";
        public string Description => "A collection of topics determined by your activity on Facebook that is used to create recommendations for you in different areas of Facebook such as News Feed, News and Watch";

        [JsonPropertyName("inferred_topics_v2")]
        public IEnumerable<StringWrapperDbObj>? Items { get; set; }
    }
}
