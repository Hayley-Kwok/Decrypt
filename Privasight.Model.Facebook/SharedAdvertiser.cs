﻿using System.Text.Json.Serialization;
using Privasight.Model.Shared.DataStructures.AbstractClasses;
using Privasight.Model.Shared.DataStructures.Interfaces;
using Privasight.Model.Shared.Display.Attributes;

namespace Privasight.Model.Facebook;

public class AdvertisersUsingYourActivity : ISingleItemListFile<SharedAdvertiser>
{
    public static readonly string Filepath = @"ads_information/advertisers_using_your_activity_or_information.json";
    public string Description =>
        @"Advertisers can choose to show their ads to certain audiences. You may see ads because an advertiser has included you in an audience based on a list of information or your interactions with their website, app or store. Advertisers can use or upload a list of information that Facebook can match to your profile.
          <a target=""_blank"" class=""text-decoration-underline"" href=""https://www.facebook.com/help/794535777607370#lookalike"">Learn More</a><br/>
          <a target=""_blank"" class=""text-decoration-underline"" href=""https://www.facebook.com/business/help/744354708981227?id=2469097953376494"">More on Custom Audience</a><br/>
          <a target=""_blank"" class=""text-decoration-underline"" href=""https://www.facebook.com/adpreferences/?section=audience_based_advertising&entry_product=information_about_you"">Change the settings on Facebook (link only accessible after you logged into Facebook) </a><br/>";

    public string Title => "Advertisers Using Your Activity or Information";

    [JsonPropertyName("custom_audiences_all_types_v2")]
    public IEnumerable<SharedAdvertiser>? Items { get; set; }
}

public class SharedAdvertiser : DbTableObj
{
    [DataListValue]
    [JsonPropertyName("advertiser_name")]
    [DetailedTableDisplayData("Advertiser Name")]
    public string? Name { get; set; }

    [JsonPropertyName("has_data_file_custom_audience")]
    [DetailedTableDisplayData("Has Custom Audience File?", "Do you have a custom audience file uploaded by the advertiser?")]
    public bool HasDataFileCustomAudience { get; set; }

    [JsonPropertyName("has_remarketing_custom_audience")]
    [DetailedTableDisplayData("Remarketing Audience?", "Are you being targeted by the advertiser?")]
    public bool HasRemarketingCustomAudience { get; set; }

    [JsonPropertyName("has_in_person_store_visit")]
    [DetailedTableDisplayData("Has in person store visit?")]
    public bool HasInPersonStoreVisit { get; set; }
}