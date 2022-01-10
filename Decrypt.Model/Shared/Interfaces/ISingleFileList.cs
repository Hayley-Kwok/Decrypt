namespace Decrypt.Model.Shared.Interfaces;

public interface ISingleListFile : IFileWrapper
{
    Type ItemsType { get; }
}

public interface ISingleListFile<T> : ISingleListFile
{
    IEnumerable<T>? Items { get; set; }
}