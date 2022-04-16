using System.Reflection;
using Privasight.Model.Shared.Display.Attributes;

namespace Privasight.Model.Shared.Helpers
{
    public static class DisplayDataAttributeHelper
    {
        public static DisplayDataAttributeDTO GetDTO(PropertyInfo propertyInfo)
        {
            var attr = propertyInfo.GetCustomAttribute<DisplayDataAttribute>();
            return new DisplayDataAttributeDTO(attr?.DisplayName ?? string.Empty, attr?.Description ?? string.Empty);
        }

        private static string TransformBoolToYesNo(bool val) => val ? "Yes" : "No";
        public static string FormatPropertyString(PropertyInfo propertyInfo, object obj)
        {
            var value = propertyInfo.GetValue(obj);

            return value switch
            {
                bool booValue => TransformBoolToYesNo(booValue),
                string strValue => strValue,
                DateTimeOffset dateTime => dateTime.ToString("yyyy-MM-dd,HH:mm:ss"),
                _ => value?.ToString() ?? "No data"
            };
        }
    }
}
