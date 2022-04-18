using Privasight.Model.Shared.DataStructures.AbstractClasses;
using Privasight.Model.Shared.Display.DataList;

namespace Privasight.Model.Shared.Helpers;

public static class DataListHelper
{
    public static IEnumerable<DataListDisplayDetails> GenerateDataListDisplay(IEnumerable<DbTableObj> dbTableObjs,
        string displayPropertyName)
    {
        var strValuesGenerationDates = new Dictionary<string, List<DateTimeOffset>>();
        foreach (var dbTableObj in dbTableObjs)
        {
            var strVal = ReflectionHelper.GetPropertyStrValue(dbTableObj, displayPropertyName);
            if (strValuesGenerationDates.TryGetValue(strVal, out var dateTimes))
            {
                dateTimes.Add(dbTableObj.GeneratedOn);
            }
            else
            {
                strValuesGenerationDates.Add(strVal, new List<DateTimeOffset> { dbTableObj.GeneratedOn });
            }
        }

        foreach (var (strVal, generationDates) in strValuesGenerationDates)
        {
            yield return new DataListDisplayDetails(strVal, generationDates);
        }
    }
}