using Privasight.Model.Shared.Attributes;

namespace Privasight.Model.Shared.AbstractClasses;

public abstract class DbTableObj
{
    [DisplayData("Generated on", "This row is from the zip file generated on this date")]
    public DateTimeOffset GeneratedOn { get; set; }
}