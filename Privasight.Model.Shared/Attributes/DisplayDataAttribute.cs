namespace Privasight.Model.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DisplayDataAttribute : Attribute
    {
        public DisplayDataAttribute(string displayName)
        {
            DisplayName = displayName;
        }

        public DisplayDataAttribute(string displayName, string description)
        {
            DisplayName = displayName;
            Description = description;
        }

        public string DisplayName { get; set; }

        public string Description { get; set; } = "";
    }

    public class DisplayDataAttributeDTO
    {
        public DisplayDataAttributeDTO(string displayName, string description)
        {
            DisplayName = displayName;
            Description = description;
        }

        public string DisplayName { get; set; }

        public string Description { get; set; }
    }
}
