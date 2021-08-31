namespace Bdaya.Firebase.Storage;

public struct UploadFileProposal : IStorageFile
{
    public string Md5Hash { get; set; }
    public string FileName { get; set; }
    public string MediaLink { get; set; }
    public string UploaderId { get; set; }
    public string OriginalFileName { get; set; }
    public DateTime UploadedAt { get; set; }
    public string BucketName { get; set; }
    public Google.Apis.Storage.v1.Data.Object StorageObject { get; set; }
    public string ObjectName { get; set; }
    public long FileSizeInBytes { get; set; }
}
