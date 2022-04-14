using Privasight.Model.Shared.Interfaces;

namespace Privasight.Model.Shared
{
    /// <summary>
    /// Root object for storing the transformed data from the zip file
    /// </summary>
    public class CompanyRoot
    {
        /// <summary>
        /// Store the transformed FileWrappers (Json object that has transformed to supported C# object)
        /// 1 FileWrapper correspond to 1 Json File in the zip file
        /// Key: Name of the type of the FileWrapper stored in value; Value: The FileWrapper Object
        /// </summary>
        public Dictionary<string, IFileWrapper> AvailableData { get; set; } = new();
    }
}
