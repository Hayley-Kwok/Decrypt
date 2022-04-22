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
        public string Description => "Advertisers whose ads you've clicked on Facebook<br/> <a target=\"_blank\" class=\"text-decoration-underline\" href=\"https://www.facebook.com/adpreferences/ad_settings\">Ad Settings on Facebook (link only accessible after you logged into Facebook) </a><br/>";

        [JsonPropertyName("history_v2")]
        public IEnumerable<InteractedAdvertiser>? Items { get; set; }
    }

    public class InteractedAdvertiser : DbTableObj
    {
        [DataListValue]
        [JsonPropertyName("title")]
        [DetailedTableDisplayData("Title", "The title of the Ad")]
        public string? AdTitle { get; set; }

        [JsonPropertyName("action")]
        [DetailedTableDisplayData(nameof(Interaction), "The interaction with the Ad")]
        public string? Interaction { get; set; }

        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        [JsonPropertyName("timestamp")]
        [DetailedTableDisplayData("Interacted on")]
        public DateTimeOffset TimeStamp { get; set; }
    }
}
