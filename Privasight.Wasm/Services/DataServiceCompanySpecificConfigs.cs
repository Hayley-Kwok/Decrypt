using Privasight.Model.Facebook;
using Privasight.Wasm.Pages;

namespace Privasight.Wasm.Services
{
    public partial class DataService
    {
        public Dictionary<AvailableCompany, Dictionary<string, Type>> AvailableFileWrappers => new()
        {
            { AvailableCompany.Facebook, FbConfig.AvailableFileWrappers }
        };

        public Dictionary<AvailableCompany, Type> DataDownloadInfo => new()
        {
            { AvailableCompany.Facebook, typeof(FBDownloadInstruction) }
        };
    }
}
