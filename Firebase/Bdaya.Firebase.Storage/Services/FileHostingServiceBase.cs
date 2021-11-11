using Bdaya.Models.Files;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;

namespace Bdaya.Firebase.Storage;

public abstract class FileHostingServiceBase<TFile> : IFirebaseStorageService
{
    private readonly FirebaseStorageSettings _settings;
    private readonly StorageClient _storageClient;

    public FileHostingServiceBase(FirebaseStorageSettings settings, StorageClient storageClient)
    {
        _settings = settings;
        _storageClient = storageClient;
    }

    public abstract Task<Dictionary<string, FirebaseStorageClientFileReply>> CheckFilesExistence(List<FirebaseStorageClientFileProposal> files);

    //public abstract string GetFileSubPath(string fileName, string contentType);

    //public abstract Task<IReadOnlyList<TFile>> AddFilesToDb(IReadOnlyList<UploadFileProposal> files, CancellationToken cancellationToken = default);

    //public abstract Task<Dictionary<string, TFile>> GetFilesByHash(IReadOnlyCollection<string> hash);

    //public virtual async Task<IReadOnlyList<UploadFileProposal>> GenerateProposals(IEnumerable<IFormFile> files, CancellationToken cancellationToken = default)
    //{
    //    if (files == null)
    //    {
    //        return Array.Empty<UploadFileProposal>();
    //    }
    //    files = files.Where(x => x is not null);
    //    var provider = new FileExtensionContentTypeProvider();

    //    var toAddList = new List<UploadFileProposal>();
    //    var existingList = new List<TFile>();
    //    var hashesList = new HashSet<string>();
    //    var now = DateTime.Now;


    //    foreach (var (file, index) in files.Select((item, index) => (item, index)))
    //    {
    //        var nameWithExt = Path.GetFileName(file.FileName).RemoveInvalidChars();
    //        var ext = Path.GetExtension(nameWithExt);
    //        var nameWithoutExt = Path.GetFileNameWithoutExtension(nameWithExt);

    //        var name = $"{nameWithoutExt}-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}{ext}";
    //        var contentType = file.ContentType;

    //        if (string.IsNullOrWhiteSpace(contentType) && !provider.TryGetContentType(name, out contentType))
    //        {
    //            throw new ArgumentException(contentType + " is not provided");
    //        }

    //        if (file.Length > 0)
    //        {
    //            using var memoryStream = new MemoryStream();
    //            await file.CopyToAsync(memoryStream, cancellationToken);
    //            memoryStream.Seek(0, SeekOrigin.Begin);
    //            var hash = (await memoryStream.GetHashMd5(cancellationToken: cancellationToken)).BytesToString();
    //            if (hashesList.Contains(hash))
    //            {
    //                continue;
    //            }
    //            else
    //            {
    //                hashesList.Add(hash);
    //            }
    //            //TODO: optimize this by hashing files first, then uploading them to firebase
    //            var prevFiles = await GetFilesByHash(new HashSet<string>() { hash });
    //            if (!prevFiles.TryGetValue(hash, out var prevFile))
    //            {
    //                var objectName = _settings.BucketSubPath + GetFileSubPath(name, contentType);

    //                var dataObject = await _storageClient.UploadObjectAsync(_settings.BucketName, objectName, contentType, memoryStream);

    //                var mediaLink = $"https://storage.googleapis.com/{_settings.BucketName}/{Uri.EscapeDataString(objectName)}";
    //                toAddList.Add(new UploadFileProposal()
    //                {
    //                    ContentType = contentType,
    //                    UploaderId = UserId,
    //                    BucketName = _settings.BucketName,
    //                    FileSizeInBytes = file.Length,
    //                    Md5Hash = hash,
    //                    MediaLink = mediaLink,
    //                    ObjectName = objectName,
    //                    StorageObject = dataObject,
    //                    UploadedAt = now,
    //                    OriginalFileName = nameWithExt,
    //                    FileName = name,
    //                });
    //            }
    //            else
    //            {
    //                existingList.Add(prevFile);
    //            }

    //        }
    //    }

    //    return toAddList;
    //}

    ///// <summary>
    ///// Uploads multiple files to a server
    ///// </summary>
    ///// <param name="UserName"></param>
    ///// <param name="files"></param>
    ///// <returns></returns>
    //public virtual async Task<IReadOnlyList<TFile>> UploadFiles(IEnumerable<IFormFile> files, CancellationToken cancellationToken = default)
    //{
    //    var toAddList = await GenerateProposals(files, cancellationToken);
    //    return (await AddFilesToDb(toAddList, cancellationToken)).Concat(existingList);
    //}
    //public virtual async Task<Google.Apis.Storage.v1.Data.Object?> GetBucketObject(IStorageFile file, CancellationToken cancellationToken = default)
    //{
    //    try
    //    {
    //        var objectName = _settings.BucketSubPath + GetFileSubPath(file.UploaderId, file.FileName, file.ContentType);
    //        var res = await _storageClient.GetObjectAsync(_settings.BucketName, objectName, cancellationToken: cancellationToken);
    //        if (res == null || res.TimeDeleted != null)
    //        {
    //            return null;
    //        }
    //        return res;
    //    }
    //    catch
    //    {
    //        return null;
    //    }
    //}

    public Task<Google.Apis.Storage.v1.Data.Object?> GetBucketObject(IFirebaseStorageFile file, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

}
