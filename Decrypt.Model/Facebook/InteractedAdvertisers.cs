using System.Text.Json.Serialization;
using Decrypt.Model.Shared.Interfaces;

namespace Decrypt.Model.Facebook
{
    public class AdvertiserYouInteractedWith : IIngestedFileComponent
    {
        public static readonly string Filepath = @"ads_information/advertisers_you've_interacted_with.json";
        public static readonly string Title = "Advertisers you have interacted with";
        public static readonly string Description = "Advertisers whose ads you've clicked on Facebook";

        [JsonPropertyName("history_v2")]
        public IEnumerable<InteractedAdvertisers>? InteractedAdvertisers { get; set; }
    }

    public class InteractedAdvertisers
    {
        [JsonPropertyName("title")]
        public string? AdTitle { get; set; }

        public string? Action { get; set; }

        public DateTimeOffset TimeStamp { get; set; }
    }
}
