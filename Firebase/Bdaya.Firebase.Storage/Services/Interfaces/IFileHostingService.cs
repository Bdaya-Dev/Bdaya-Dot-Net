using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Bdaya.Firebase.Storage;

public interface IFileHostingService<TFile>
{

    Task<Google.Apis.Storage.v1.Data.Object?> GetBucketObject(IStorageFile file, CancellationToken cancellationToken = default);
    Task<Dictionary<string, TFile>> GetFilesByHash(string userId, IReadOnlyCollection<string> hash);
    Task<IEnumerable<TFile>> UploadFiles(string UserId, IEnumerable<IFormFile> files, CancellationToken cancellationToken = default);
}
