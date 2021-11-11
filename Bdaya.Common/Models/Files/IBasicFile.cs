namespace Bdaya.Models.Files;

/// <summary>
/// Represents an arbitrary file
/// </summary>
public interface IBasicFile
{
    /// <summary>
    /// The md5 hash of the file
    /// </summary>
    /// <remarks>This needs to be indexed</remarks>
    string Md5Hash { get; }

    /// <summary>
    /// Original file name (with extension) without any prefix/postfix
    /// </summary>
    string OriginalFileName { get; }

    /// <summary>
    /// The file name (with extension)
    /// </summary>
    string FileName { get; }

    /// <summary>
    /// The content type (based on the extension or file data)
    /// </summary>
    string? ContentType { get; }

    /// <summary>
    /// File size in bytes
    /// </summary>
    long FileSizeInBytes { get; }

}