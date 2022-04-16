using Privasight.Model.Shared.Display.Attributes;

namespace Privasight.Model.Shared.DataStructures.AbstractClasses;

/// <summary>
/// A wrapper object for Items in ISingleItemListFile 
/// </summary>
public abstract class DbTableObj
{
    [DisplayData("Generated on", "The last modified date of the zip file")]
    public DateTimeOffset GeneratedOn { get; set; }
}