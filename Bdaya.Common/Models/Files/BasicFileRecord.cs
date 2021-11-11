namespace Bdaya.Models.Files;

public record BasicFileRecord(string Md5Hash,
string OriginalFileName,
string FileName,
string? ContentType,
long FileSizeInBytes) : IBasicFile;