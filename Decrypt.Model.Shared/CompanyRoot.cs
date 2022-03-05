using Decrypt.Model.Shared.Interfaces;

namespace Decrypt.Model.Shared
{
    public class CompanyRoot
    {
        public Dictionary<string, IFileWrapper> AvailableData { get; set; } = new();
    }
}
