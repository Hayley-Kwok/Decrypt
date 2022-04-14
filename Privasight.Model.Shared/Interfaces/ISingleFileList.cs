using Privasight.Model.Shared.AbstractClasses;

namespace Privasight.Model.Shared.Interfaces;

/// <summary>
/// A Helper interface for ISingleItemListFile
/// </summary>
public interface ISingleItemListFile : IFileWrapper
{
    /// <summary>
    /// The T in ISingleItemListFile<T>
    /// </summary>
    Type ItemsType { get; }
}

/// <summary>
/// The file wrapper for Json file that is purely a list of object
/// </summary>
/// <typeparam name="T">Type of the items in the list</typeparam>
public interface ISingleItemListFile<T> : ISingleItemListFile where T : DbTableObj
{
    IEnumerable<T>? Items { get; set; }
}