using System.Text.Json.Serialization;
using Decrypt.Model.Converters;
using Decrypt.Model.Shared;
using Decrypt.Model.Shared.Interfaces;

namespace Decrypt.Model.Facebook
{
    public class AdvertiserYouInteractedWith : ISingleItemListFile<InteractedAdvertiser>
    {
        public static readonly string Filepath = @"ads_information/advertisers_you've_interacted_with.json";

        public string Title => "Advertisers you have interacted with";
        public string Description => "Advertisers whose ads you've clicked on Facebook";
        
        public Type ItemsType => typeof(InteractedAdvertiser);

        [JsonPropertyName("history_v2")]
        public IEnumerable<InteractedAdvertiser>? Items { get; set; }
    }

    public class InteractedAdvertiser
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
