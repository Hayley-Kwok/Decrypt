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
        public static readonly string NameDisplayName = "Advertiser Name";
        [JsonPropertyName("advertiser_name")]
		public string? Name { get; set; }

        public static readonly string HasDataFileCustomAudienceDisplayName = "Has Custom Audience File?";
        [JsonPropertyName("has_data_file_custom_audience")]
        public bool HasDataFileCustomAudience { get; set; }

        public static readonly string HasRemarketingCustomAudienceDisplayName = "Remarketing Audience?";
        [JsonPropertyName("has_remarketing_custom_audience")]
        public bool HasRemarketingCustomAudience { get; set; }

        public static readonly string HasInPersonStoreVisitDisplayName = "Has in person store visit?";
        [JsonPropertyName("has_in_person_store_visit")]
        public bool HasInPersonStoreVisit { get; set; }
	}
}
