using Privasight.Model.Shared.DataStructures.AbstractClasses;
using Privasight.Model.Shared.Display.DataList;

namespace Privasight.Model.Shared.Helpers;

public static class DataListHelper
{
    public static IEnumerable<DataListDisplayDetails> GenerateDataListDisplay(IEnumerable<DbTableObj> dbTableObjs,
        string displayPropertyName)
    {
        
        var tableObjs = dbTableObjs as DbTableObj[] ?? dbTableObjs.ToArray();
        var objsByGenerationDate = tableObjs.ToLookup(
            d => ReflectionHelper.GetPropertyStrValue(d, displayPropertyName),
            d => d.GeneratedOn);

        foreach (var objByGenerationDate in objsByGenerationDate)
        {
            yield return new DataListDisplayDetails(objByGenerationDate.Key, objByGenerationDate);
        }
    }
}