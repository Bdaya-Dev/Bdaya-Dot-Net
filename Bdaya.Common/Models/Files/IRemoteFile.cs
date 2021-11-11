namespace Bdaya.Models.Files;
/// <summary>
/// Represents a file uploaded to a remote server
/// </summary>
public interface IRemoteFile
{

    /// <summary>
    /// The link to access the file on the remote server
    /// </summary>
    /// <remarks>MUST be a direct download</remarks>
    string MediaLink { get; }

    /// <summary>
    /// When was the file uploaded to the remote server
    /// </summary>
    DateTime UploadedAt { get; }
}
