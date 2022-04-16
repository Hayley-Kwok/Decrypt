using Privasight.Model.Shared.Display.Attributes;

namespace Privasight.Model.Shared.DataStructures.AbstractClasses;

/// <summary>
/// A wrapper object for Items in ISingleItemListFile 
/// </summary>
public abstract class DbTableObj
{
    [DisplayData("Generated on", "This row is from the zip file generated on this date")]
    public DateTimeOffset GeneratedOn { get; set; }
}