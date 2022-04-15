using System.Reflection;

namespace Privasight.Model.Shared.Helpers
{
    public static class ReflectionHelper
    {
        public static IEnumerable<PropertyInfo> GetProperties(object obj) => obj.GetType().GetProperties();

        public static string GetPropertyStrValue(object obj, string propName) =>
            obj.GetType().GetProperty(propName)?.GetValue(obj)?.ToString() ?? "";

    }
}
