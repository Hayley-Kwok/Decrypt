using Privasight.Model.Shared.DataStructures.AbstractClasses;
using Privasight.Model.Shared.Display.Attributes;
using Privasight.Model.Shared.Display.DataList;

namespace Privasight.Model.Shared.Helpers;

public static class DataListHelper
{
    private static string GetDataListValue(Type tItem, object obj)
    {
        foreach (var prop in tItem.GetProperties())
        {
            var attrs = prop.GetCustomAttributes(true).OfType<DataListValueAttribute>();

            if (attrs.Any())
            {
                return prop.GetValue(obj)?.ToString() ?? throw new Exception($"Cannot get the value from {prop} for DataListValue");
            }
        }

        throw new Exception("Cannot find any property with the DataListValueAttribute");
    }
    public static IEnumerable<DataListDisplayDetails> GenerateDataListDisplay(IEnumerable<DbTableObj> dbTableObjs,
        Type tItem)
    {
        var tableObjs = dbTableObjs as DbTableObj[] ?? dbTableObjs.ToArray();

        var objsByGenerationDate = tableObjs.ToLookup(
            d => GetDataListValue(tItem, d),
            d => d.GeneratedOn);

        foreach (var objByGenerationDate in objsByGenerationDate)
        {
            yield return new DataListDisplayDetails(objByGenerationDate.Key, objByGenerationDate);
        }
    }
}