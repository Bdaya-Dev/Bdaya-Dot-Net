using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Bdaya.Firebase.Storage;

public interface IFileHostingService<TFile>
{
    
    Task<Google.Apis.Storage.v1.Data.Object?> GetBucketObject(IStorageFile file, CancellationToken? cancellationToken = null);    
    Task<IEnumerable<TFile>> UploadFiles(string UserId, IEnumerable<IFormFile> files, CancellationToken? cancellationToken = null);
}
