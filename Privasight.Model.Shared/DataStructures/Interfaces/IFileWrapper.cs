namespace Privasight.Model.Shared.DataStructures.Interfaces;

/// <summary>
/// Wrapper interface for each Json File from the zip folder.
/// </summary>
public interface IFileWrapper
{
    public static readonly string Filepath = "";

    string Description { get; }
    string Title { get; }
}