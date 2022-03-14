using System.IO.Compression;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Privasight.Model.Facebook;
using Privasight.Model.Facebook.Data;
using Privasight.Model.Shared;
using Privasight.Model.Shared.Converters;
using Privasight.Model.Shared.Interfaces;

namespace Privasight.Wasm.Services
{
    public static class DataAccessHelper
    {
        private const long MaxFileSize = 4294967296; // 4 GB
        public const string SqliteDbFilename = "app.db";

        public static async Task<(Dictionary<string, IFileWrapper> newData, DateTimeOffset generationDate)> TransformJsonToObj(InputFileChangeEventArgs e, ConfigService configService)
        {
            var newData = new Dictionary<string, IFileWrapper>();
            var options = new JsonSerializerOptions();
            options.Converters.Add(new FbConverter());

            //TODO: figure out a better way to do this
            await using var stream = new MemoryStream();
            await e.File.OpenReadStream(MaxFileSize).CopyToAsync(stream);

            var generationDate = e.File.LastModified;

            using var archive = new ZipArchive(stream);
            foreach (var entry in archive.Entries)
            {
                if (!configService.AvailableFileWrappers.TryGetValue(entry.FullName, out var wrapperType)) continue;
                var unzippedEntryStream = entry.Open();

                if (await JsonSerializer.DeserializeAsync(unzippedEntryStream, wrapperType, options) is IFileWrapper wrapperObj)
                {
                    newData.TryAdd(wrapperType.Name, wrapperObj);
                }
            }

            return (newData, generationDate);
		}

        public static void AddGenerationDate(Dictionary<string, IFileWrapper> newData, DateTimeOffset generationDate)
        {
            foreach (var wrapper in newData.Values)
            {
                if (wrapper is ISingleItemListFile)
                {
                    //todo improve this
                    dynamic singleItemList = wrapper;
                    foreach (var item in singleItemList.Items)
                    {
                        item.GeneratedOn = generationDate;
                    }
                }
            }
        }

        public static async Task LoadDataIntoDatabase(Dictionary<string, IFileWrapper> newData, IDbContextFactory<FbContext> dbContextFactory)
        {
            await using var db = await GetDbContext(dbContextFactory);

            foreach (var pair in newData)
            {
                switch (pair.Key)
                {
                    case nameof(AdvertiserYouInteractedWith):
                        if (pair.Value is AdvertiserYouInteractedWith { Items: { } } advertiserYouInteractedWith)
                        {
                            await db.InteractedAdvertisers.AddRangeAsync(advertiserYouInteractedWith.Items);
                        }
                        break;
                    case nameof(AdvertisersUsingYourActivity):
                        if (pair.Value is AdvertisersUsingYourActivity {Items: { } } advertisersUsingYourActivity)
                        {
                        	// await db.SharedAdvertisers.AddRangeAsync(advertisersUsingYourActivity.Items);
                        }
                        break;
                    case nameof(InferredTopics):
                        break;
                }
            }

            await db.SaveChangesAsync();
        }

        public static async Task GetLatestAvailableData(IDbContextFactory<FbContext> dbContextFactory, DataService dataService)
        {
            await using var db = await GetDbContext(dbContextFactory);
            dataService.FbRoot ??= new CompanyRoot();
            dataService.FbRoot.AvailableData = new Dictionary<string, IFileWrapper>();

            // var sharedAdvertisers = db.SharedAdvertisers.ToList();
            // dataService.FbRoot.AvailableData.Add(nameof(AdvertisersUsingYourActivity), new AdvertisersUsingYourActivity{ Items = sharedAdvertisers});

            var interactedAdvertisers = db.InteractedAdvertisers.ToList();
            dataService.FbRoot.AvailableData.Add(nameof(AdvertiserYouInteractedWith), new AdvertiserYouInteractedWith() { Items = interactedAdvertisers });
        }

        public static async Task<FbContext> GetDbContext(IDbContextFactory<FbContext> dbContextFactory)
        {
            var db = await dbContextFactory.CreateDbContextAsync();
            // await db.Database.EnsureDeletedAsync();
            await db.Database.EnsureCreatedAsync();
            // await db.SaveChangesAsync();
            return db;
        }
    }
}
