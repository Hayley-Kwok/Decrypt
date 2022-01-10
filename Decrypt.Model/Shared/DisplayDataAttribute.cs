using System.Reflection;

namespace Decrypt.Model.Shared
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DisplayDataAttribute : Attribute
    {
        public DisplayDataAttribute()
        {
            
        }

        public DisplayDataAttribute(string displayName)
        {
            DisplayName = displayName;
        }

        public DisplayDataAttribute(string displayName, string description)
        {
            DisplayName = displayName;
            Description = description;
        }

        public string? DisplayName { get; set; }

        public string? Description { get; set; }
    }

    public static class DisplayDataAttributeHelper
    {
        public static DisplayDataAttributeDTO GetDTO(PropertyInfo propertyInfo)
        {
            var attr = propertyInfo.GetCustomAttribute<DisplayDataAttribute>();
            return new DisplayDataAttributeDTO(attr?.DisplayName, attr?.Description);
        }

        public static IEnumerable<PropertyInfo> GetPropertiesReflection(object obj) => obj.GetType().GetProperties();

        private static string TransformBoolToYesNo(bool val) => val ? "Yes" : "No";
        public static string FormatPropertyString(PropertyInfo propertyInfo, object obj)
        {
            var value = propertyInfo.GetValue(obj);

            return value switch
            {
                bool booValue => TransformBoolToYesNo(booValue),
                string strValue => strValue,
                _ => value?.ToString() ?? "NULL"
            };
        }
    }

    public class DisplayDataAttributeDTO
    {
        public DisplayDataAttributeDTO(string? displayName, string? description)
        {
            DisplayName = displayName;
            Description = description;
        }
        public string? DisplayName { get; set; }

        public string? Description { get; set; }
    }
}
