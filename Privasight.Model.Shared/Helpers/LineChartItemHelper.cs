using Privasight.Model.Shared.DataStructures.AbstractClasses;
using Privasight.Model.Shared.Display;

namespace Privasight.Model.Shared.Helpers;

public static class LineChartItemHelper
{
    public static IEnumerable<LineChartItem> ConvertToLineChartItems(IEnumerable<DbTableObj> dbTableObjs)
    {
        var objByGenerationDate = dbTableObjs.ToLookup(d => d.GeneratedOn);
        foreach (var  generationDateObject in objByGenerationDate)
        {
            yield return new LineChartItem(generationDateObject.Key.DateTime, generationDateObject.Count());
        }
    }
}