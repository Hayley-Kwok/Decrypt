using System.Text.Json.Serialization;

//path: ads_information/advertisers_using_your_activity_or_information.json
namespace Decrypt.Model.Facebook
{
    public class AdvertisersUsingYourActivity
    {
        public static readonly string Filepath = @"ads_information/advertisers_using_your_activity_or_information.json";

        [JsonPropertyName("custom_audiences_all_types_v2")]
        public IEnumerable<SharedAdvertiser>? SharedAdvertisers { get; set; }
    }

    public class SharedAdvertiser
	{
        [JsonPropertyName("advertiser_name")]
		public string? Name { get; set; }

        [JsonPropertyName("has_data_file_custom_audience")]
        public bool HasDataFileCustomAudience { get; set; }

        [JsonPropertyName("has_remarketing_custom_audience")]
        public bool HasRemarketingCustomAudience { get; set; }

        [JsonPropertyName("has_in_person_store_visit")]
        public bool HasInPersonStoreVisit { get; set; }
	}

    public static class SharedAdvertiserDisplayNames
    {
        public const string Name = "Advertiser Name";
        public const string HasDataFileCustomAudience = "Has Custom Audience File?";
        public const string HasRemarketingCustomAudience = "Remarketing Audience?";
        public const string HasInPersonStoreVisit = "Has in person store visit?";
    }
}
