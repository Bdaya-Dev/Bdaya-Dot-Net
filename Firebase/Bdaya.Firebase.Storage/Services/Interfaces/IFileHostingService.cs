using Bdaya.Models.Files;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Bdaya.Firebase.Storage;

public interface IFirebaseStorageService
{

    //Task<Google.Apis.Storage.v1.Data.Object?> GetBucketObject(IFirebaseStorageFile file, CancellationToken cancellationToken = default);
    //Task<Dictionary<string, IFirebaseStorageFile>> GetFilesByHash(IReadOnlyCollection<string> hash);
    //Task<IEnumerable<TFile>> UploadFiles(IEnumerable<IFormFile> files, CancellationToken cancellationToken = default);

    Task<Dictionary<string, FirebaseStorageClientFileReply>> CheckFilesExistence(List<FirebaseStorageClientFileProposal> files);
}

