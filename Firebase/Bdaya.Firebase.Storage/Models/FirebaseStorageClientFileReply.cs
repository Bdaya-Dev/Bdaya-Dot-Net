namespace Bdaya.Models.Files;

/// <summary>
/// A record representing where the file should be uploaded at server side
/// The client gets the media link
/// </summary>
/// <param name="Md5Hash"></param>
/// <param name="FileName"></param>
/// <param name="BucketName"></param>
/// <param name="ObjectName"></param>
public record FirebaseStorageClientFileReply(
    string Md5Hash,
    string FileName,
    long FileSizeInBytes,
    string BucketName,
    string ObjectName,
    string OriginalFileName,
    string? ContentType) : IBasicFile, IFirebaseStorageFile;