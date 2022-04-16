namespace Privasight.Model.Facebook;

public static class FbConfig
{
    /// <summary>
    /// The supported data type (FileWrapper) for the FB zip file
    /// Key: filepath of the FileWrapper in the zip file
    /// Value: type of the file wrapper
    /// </summary>
    public static readonly Dictionary<string, Type> AvailableFileWrappers = new()
    {
        { AdvertisersUsingYourActivity.Filepath, typeof(AdvertisersUsingYourActivity) },
        { AdvertiserYouInteractedWith.Filepath, typeof(AdvertiserYouInteractedWith) },
        { InferredTopics.Filepath, typeof(InferredTopics) }
    };
}