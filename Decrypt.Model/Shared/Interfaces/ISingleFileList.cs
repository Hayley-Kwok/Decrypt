namespace Decrypt.Model.Shared.Interfaces;

public interface ISingleItemListFile : IFileWrapper
{
    Type ItemsType { get; }
}

public interface ISingleItemListFile<T> : ISingleItemListFile
{
    IEnumerable<T>? Items { get; set; }
}