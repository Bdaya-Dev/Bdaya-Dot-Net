namespace Bdaya.Models.Files;

public record RemoteFileRecord(string Md5Hash,
string OriginalFileName,
string FileName,
string MediaLink,
DateTime UploadedAt,
string? ContentType,
long FileSizeInBytes) :
    BasicFileRecord(
        Md5Hash: Md5Hash,
        OriginalFileName: OriginalFileName,
        FileName: FileName,
        ContentType: ContentType,
        FileSizeInBytes: FileSizeInBytes), IRemoteFile;
