using System.Text.Json.Serialization;
using Decrypt.Model.Shared.Interfaces;

namespace Decrypt.Model.Facebook
{
    public class AdvertisersUsingYourActivity : IFileWrapper
    {
        public static readonly string Filepath = @"ads_information/advertisers_using_your_activity_or_information.json";
        public static readonly string Title = "Advertisers Using Your Activity or Information";

        public static readonly string Description =
            "Advertisers can choose to show their ads to certain audiences. You may see ads because an advertiser has included you in an audience based on a list of information or your interactions with their website, app or store. Advertisers can use or upload a list of information that Facebook can match to your profile.";


        [JsonPropertyName("custom_audiences_all_types_v2")]
        public IEnumerable<SharedAdvertiser>? SharedAdvertisers { get; set; }
    }

    public class SharedAdvertiser
    {

        public static readonly string NameDisplayName = "Advertiser Name";
        [JsonPropertyName("advertiser_name")]
		public string? Name { get; set; }

        public static readonly string HasDataFileCustomAudienceDescription = "Do you have a custom audience file uploaded by the advertiser?";
        public static readonly string HasDataFileCustomAudienceDisplayName = "Has Custom Audience File?";
        [JsonPropertyName("has_data_file_custom_audience")]
        public bool HasDataFileCustomAudience { get; set; }

        public static readonly string HasRemarketingCustomAudienceDescription = "Are you being targeted by the advertiser?";
        public static readonly string HasRemarketingCustomAudienceDisplayName = "Remarketing Audience?";
        [JsonPropertyName("has_remarketing_custom_audience")]
        public bool HasRemarketingCustomAudience { get; set; }

        public static readonly string HasInPersonStoreVisitDisplayName = "Has in person store visit?";
        [JsonPropertyName("has_in_person_store_visit")]
        public bool HasInPersonStoreVisit { get; set; }
	}
}
