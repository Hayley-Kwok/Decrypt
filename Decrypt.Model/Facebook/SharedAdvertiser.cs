using System.Text.Json.Serialization;

//path: ads_information/advertisers_using_your_activity_or_information.json
namespace Decrypt.Model.Facebook
{
    public class AdvertisersUsingYourActivity
    {
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
}
