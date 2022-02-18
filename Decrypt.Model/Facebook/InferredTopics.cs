﻿using System.Text.Json.Serialization;
using Decrypt.Model.Shared.Interfaces;

namespace Decrypt.Model.Facebook
{
    public class InferredTopics : ISingleStringList
    {
        public static readonly string Filepath = @"your_topics/your_topics.json";

        public string Title => "Inferred Topics";
        public string Description => "A collection of topics determined by your activity on Facebook that is used to create recommendations for you in different areas of Facebook such as News Feed, News and Watch";

        [JsonPropertyName("inferred_topics_v2")]
        public IEnumerable<string>? Items { get; set; }
    }
}
