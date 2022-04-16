using System.Text.Json.Serialization;
using Privasight.Model.Shared.Converters;
using Privasight.Model.Shared.DataStructures.AbstractClasses;
using Privasight.Model.Shared.DataStructures.Interfaces;
using Privasight.Model.Shared.Display.Attributes;

namespace Privasight.Model.Facebook
{
    public class AdvertiserYouInteractedWith : ISingleItemListFile<InteractedAdvertiser>
    {
        public static readonly string Filepath = @"ads_information/advertisers_you've_interacted_with.json";

        public string Title => "Advertisers you have interacted with";
        public string Description => "Advertisers whose ads you've clicked on Facebook";

        [JsonPropertyName("history_v2")]
        public IEnumerable<InteractedAdvertiser>? Items { get; set; }
    }

    public class InteractedAdvertiser : DbTableObj
    {
        [JsonPropertyName("title")]
        [DisplayData("Title", "The title of the Ad")]
        public string? AdTitle { get; set; }

        [JsonPropertyName("action")]
        [DisplayData(nameof(Interaction), "The interaction with the Ad")]
        public string? Interaction { get; set; }

        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        [JsonPropertyName("timestamp")]
        [DisplayData("Interacted on")]
        public DateTimeOffset TimeStamp { get; set; }
    }
}
