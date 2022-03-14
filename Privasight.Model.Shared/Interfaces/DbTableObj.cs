namespace Privasight.Model.Shared.Interfaces;

public abstract class DbTableObj
{
    public int Id { get; set; }

    [DisplayData("Generated on", "This row is from the zip file generated on this date")]
    public DateTimeOffset GeneratedOn { get; set; }
}