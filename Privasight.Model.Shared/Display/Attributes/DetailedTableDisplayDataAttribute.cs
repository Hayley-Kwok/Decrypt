namespace Privasight.Model.Shared.Display.Attributes
{
    /// <summary>
    /// Specific the DisplayName and Description of each property for the display in DetailedTable
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DetailedTableDisplayDataAttribute : Attribute
    {
        public DetailedTableDisplayDataAttribute(string displayName)
        {
            DisplayName = displayName;
        }

        public DetailedTableDisplayDataAttribute(string displayName, string description)
        {
            DisplayName = displayName;
            Description = description;
        }

        public string DisplayName { get; set; }

        public string Description { get; set; } = "";
    }

    public record DetailedTableDisplayDataAttributeDTO(string DisplayName, string Description);
}
