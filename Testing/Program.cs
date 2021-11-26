// See https://aka.ms/new-console-template for more information

using System.IO.Compression;
using Decrypt.Model.Facebook;
using System.Text.Json;

Console.WriteLine("Hello, World!");

var Options = new JsonSerializerOptions();

await using var stream = new FileStream(@"C:\Users\wingk\hayley\uni\cs_year3_Sem1\diss\data\fb-json\ads_information\advertisers_using_your_activity_or_information.json", FileMode.Open);
// var results = await JsonSerializer.DeserializeAsync<AdvertisersUsingYourActivity>(stream, Options);
//
// foreach (var result in results.SharedAdvertisers)
// {
//     Console.WriteLine(result.Name);
// }

var zipPath = @"C:\Users\wingk\hayley\uni\cs_year3_Sem1\diss\data\fb-json\ads_information.zip";
using (ZipArchive archive = ZipFile.OpenRead(zipPath))
{
    foreach (ZipArchiveEntry entry in archive.Entries)
    {
        if (entry.FullName.EndsWith("advertisers_using_your_activity_or_information.json", StringComparison.OrdinalIgnoreCase))
        {
            var unzippedEntryStream = entry.Open();
            var results = await JsonSerializer.DeserializeAsync<AdvertisersUsingYourActivity>(unzippedEntryStream, Options);
        }
    }
}
