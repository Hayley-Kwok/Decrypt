namespace Privasight.Model.Facebook;

public static class FbConfig
{
    public static readonly Dictionary<string, Type> AvailableFileWrappers = new()
    {
        { AdvertisersUsingYourActivity.Filepath, typeof(AdvertisersUsingYourActivity) },
        { AdvertiserYouInteractedWith.Filepath, typeof(AdvertiserYouInteractedWith) },
        { InferredTopics.Filepath, typeof(InferredTopics) }
    };
}