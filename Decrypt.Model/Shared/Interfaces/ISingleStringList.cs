namespace Decrypt.Model.Shared.Interfaces
{
    public interface ISingleStringList : IFileWrapper
    {
        IEnumerable<string>? Items { get; set; }
    }
}
