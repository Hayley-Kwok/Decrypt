using Privasight.Model.Shared.Interfaces;

namespace Privasight.Model.Shared
{
    public class CompanyRoot
    {
        public Dictionary<string, IFileWrapper> AvailableData { get; set; } = new();
    }
}
