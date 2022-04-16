using Privasight.Model.Shared.DataStructures.AbstractClasses;

namespace Privasight.Model.Shared.DataStructures.Interfaces;

/// <summary>
/// A Helper interface for ISingleItemListFile
/// </summary>
public interface ISingleItemListFile : IFileWrapper
{
}

/// <summary>
/// The file wrapper for Json file that is purely a list of object
/// </summary>
/// <typeparam name="T">Type of the items in the list</typeparam>
public interface ISingleItemListFile<T> : ISingleItemListFile where T : DbTableObj
{
    IEnumerable<T>? Items { get; set; }
}

public static class SingleItemListFileHelper {
    /// <summary>
    /// Get the T in the ISingleItemListFile<T> of singleItemList
    /// </summary>
    /// <param name="singleItemList"></param>
    /// <returns>T in singleItemList</returns>
    /// <exception cref="Exception">Throw exception when singleItemList doesn't implement ISingleItemListFile<T></exception>
    public static Type GetItemsT(ISingleItemListFile singleItemList)
    {
        foreach (var interfaceType in singleItemList.GetType().GetInterfaces())
        {
            if (interfaceType.IsGenericType &&
                interfaceType.GetGenericTypeDefinition()
                == typeof(ISingleItemListFile<>))
            {
                return interfaceType.GetGenericArguments()[0];
            }
        }

        throw new Exception("singleItemList does not implement ISingleItemListFile<T>");
    }
}