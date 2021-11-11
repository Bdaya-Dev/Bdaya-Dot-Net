namespace Bdaya.Models.Files;

public record FirebaseStorageFileRecord(
    string Md5Hash,
    string FileName,
    string MediaLink,
    string OriginalFileName,
    DateTime UploadedAt,
    long FileSizeInBytes,
    string? ContentType,
    string BucketName,
    string ObjectName) : RemoteFileRecord(
        OriginalFileName: OriginalFileName,
        Md5Hash: Md5Hash,
        FileName: FileName,
        MediaLink: MediaLink,
        UploadedAt: UploadedAt,
        ContentType: ContentType,
        FileSizeInBytes: FileSizeInBytes), IFirebaseStorageFile, IRemoteFile;
