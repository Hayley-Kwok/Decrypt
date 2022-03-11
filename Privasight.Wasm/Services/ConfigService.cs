using Privasight.Model.Facebook;

namespace Privasight.Wasm.Services;

public class ConfigService
{
    public readonly Dictionary<string, Type> AvailableFileWrappers = new()
    {
        { AdvertisersUsingYourActivity.Filepath, typeof(AdvertisersUsingYourActivity) },
        { AdvertiserYouInteractedWith.Filepath, typeof(AdvertiserYouInteractedWith) },
        { InferredTopics.Filepath, typeof(InferredTopics) }
    };
}