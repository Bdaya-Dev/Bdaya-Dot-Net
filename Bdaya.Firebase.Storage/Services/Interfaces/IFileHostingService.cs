using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Bdaya.Firebase.Storage;

public interface IFileHostingService<TFile>
{
    Task<bool> CheckFileExistenceInBucket(IStorageFile file);
    Task<string?> GetBucketMd5Hash(IStorageFile file);
    Task<IEnumerable<TFile>> UploadFiles(string UserId, IEnumerable<IFormFile> files);
}
