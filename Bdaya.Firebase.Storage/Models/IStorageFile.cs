using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdaya.Firebase.Storage;
public interface IStorageFile
{
    /// <summary>
    /// This needs to be indexed
    /// </summary>
    string Md5Hash { get; set; }
    string FileName { get; set; }
    string MediaLink { get; set; }
    string UploaderId { get; set; }
    string OriginalFileName { get; set; }
    public string ContentType { get; set; }
    DateTime UploadedAt { get; set; }
    string BucketName { get; set; }
    string ObjectName { get; set; }
    long FileSizeInBytes { get; set; }

}
