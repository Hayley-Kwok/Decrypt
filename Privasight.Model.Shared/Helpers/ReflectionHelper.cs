using System.Reflection;
using Privasight.Model.Shared.Display;

namespace Privasight.Model.Shared.Helpers
{
    public static class ReflectionHelper
    {
        public static IEnumerable<PropertyInfo> GetProperties(object obj) => obj.GetType().GetProperties();

        public static string GetTitleFromFileWrapper(Type fileWrapperType)
        {
            return fileWrapperType.GetProperty("Title")?.GetValue(null)?.ToString()
                   ?? throw new Exception("cannot find the file wrapper");
        }

        public static IEnumerable<FileWrapperTypeDisplayDetails> GetDisplayDetailsFromFileWrapperTypes(
            IEnumerable<Type> fileWrapperTypes)
        {
            foreach (var fileWrapperType in fileWrapperTypes)
            {
                var title = GetTitleFromFileWrapper(fileWrapperType);
                yield return new FileWrapperTypeDisplayDetails(fileWrapperType.Name, title);
            }
        }
    }
}