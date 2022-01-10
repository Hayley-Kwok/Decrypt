using System.Text.Json.Serialization;
using Decrypt.Model.Shared.Interfaces;

namespace Decrypt.Model.Facebook
{
    public class AdvertiserYouInteractedWith : ISingleListFile<InteractedAdvertiser>
    {
        public static readonly string Filepath = @"ads_information/advertisers_you've_interacted_with.json";
        public string Description => "Advertisers whose ads you've clicked on Facebook";

        public string Title => "Advertisers you have interacted with";

        public Type ItemsType => typeof(InteractedAdvertiser);

        [JsonPropertyName("history_v2")]
        public IEnumerable<InteractedAdvertiser>? Items { get; set; }
    }

    public class InteractedAdvertiser
    {
        [JsonPropertyName("title")]
        public string? AdTitle { get; set; }

        public string? Action { get; set; }

        public DateTimeOffset TimeStamp { get; set; }
    }
}
