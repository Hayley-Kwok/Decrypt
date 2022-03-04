using Decrypt.Model.Shared.Interfaces;

namespace Decrypt.Model.Facebook;

public class FbRoot
{
    public Dictionary<string, IFileWrapper> AvailableData { get; set; } = new();
}