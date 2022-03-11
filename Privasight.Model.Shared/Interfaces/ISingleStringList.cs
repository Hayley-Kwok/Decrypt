namespace Privasight.Model.Shared.Interfaces
{
    public interface ISingleStringList : IFileWrapper
    {
        IEnumerable<string>? Items { get; set; }
    }
}
