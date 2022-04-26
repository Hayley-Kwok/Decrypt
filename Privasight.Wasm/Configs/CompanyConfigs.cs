using Privasight.Model.Facebook;
using Privasight.Wasm.Pages;

namespace Privasight.Wasm.Configs
{
    public static class CompanyConfigs
    {
        public static Dictionary<AvailableCompany, Dictionary<string, Type>> AvailableFileWrappers => new()
        {
            { AvailableCompany.Facebook, FbConfig.AvailableFileWrappers }
        };

        public static Dictionary<AvailableCompany, Type> DataDownloadInfo => new()
        {
            { AvailableCompany.Facebook, typeof(FBDownloadInstruction) }
        };
    }
}
