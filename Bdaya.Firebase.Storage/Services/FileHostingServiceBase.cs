using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace Bdaya.Firebase.Storage;

public abstract class FileHostingServiceBase<TFile> : IFileHostingService<TFile> where TFile : IStorageFile
{
    private readonly FirebaseStorageSettings _settings;
    private readonly StorageClient _storageClient;

    public FileHostingServiceBase(IOptions<FirebaseStorageSettings> settings, StorageClient storageClient)
    {
        _settings = settings.Value;
        _storageClient = storageClient;
    }


    public abstract string GetFileSubPath(string UploaderId, string fileName);
    public abstract Task<IEnumerable<TFile>> AddFilesToDb(IReadOnlyList<UploadFileProposal> files);
    public abstract Task<TFile> GetFileByHash(string hash);


    /// <summary>
    /// Uploads multiple files to a server
    /// </summary>
    /// <param name="UserName"></param>
    /// <param name="files"></param>
    /// <returns></returns>
    public virtual async Task<IEnumerable<TFile>> UploadFiles(string? UserId, IEnumerable<IFormFile> files)
    {
        if (UserId == null)
        {
            UserId = "Anonymous";
        }
        if (files == null)
        {
            return Array.Empty<TFile>();
        }

        var provider = new FileExtensionContentTypeProvider();

        var toAddList = new List<UploadFileProposal>();
        var existingList = new List<TFile>();
        var hashesList = new HashSet<string>();
        var now = DateTime.Now;
        foreach (var (file, index) in files.Select((item, index) => (item, index)))
        {
            var nameWithExt = Path.GetFileName(file.FileName).RemoveInvalidChars();
            var ext = Path.GetExtension(nameWithExt);
            var nameWithoutExt = Path.GetFileNameWithoutExtension(nameWithExt);

            var name = $"{nameWithoutExt}-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}{ext}";
            var contentType = file.ContentType;

            if (string.IsNullOrWhiteSpace(contentType) && !provider.TryGetContentType(name, out contentType))
            {
                throw new ArgumentException(contentType + " is not provided");
            }

            if (file.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                var hash = (await memoryStream.GetHashMd5()).BytesToString();
                if (hashesList.Contains(hash))
                {
                    continue;
                }
                else
                {
                    hashesList.Add(hash);
                }
                var prevFile = await GetFileByHash(hash);
                if (prevFile == null)
                {
                    var objectName = _settings.BucketSubPath + GetFileSubPath(UserId, name);
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    var dataObject = await _storageClient.UploadObjectAsync(_settings.BucketName, objectName, contentType, memoryStream);

                    var mediaLink = $"https://storage.googleapis.com/{_settings.BucketName}/{Uri.EscapeDataString(objectName)}";
                    toAddList.Add(new UploadFileProposal()
                    {
                        UploaderId = UserId,
                        BucketName = _settings.BucketName,
                        FileSizeInBytes = file.Length,
                        Md5Hash = hash,
                        MediaLink = mediaLink,
                        ObjectName = objectName,
                        StorageObject = dataObject,
                        UploadedAt = now,
                        OriginalFileName = nameWithExt,
                        FileName = name,
                    });
                }
                else
                {
                    existingList.Add(prevFile);
                }

            }
        }
        return (await AddFilesToDb(toAddList)).Concat(existingList);
    }
    public virtual async Task<string?> GetBucketMd5Hash(IStorageFile file)
    {
        try
        {
            var objectName = _settings.BucketSubPath + GetFileSubPath(file.UploaderId, file.FileName);
            var res = await _storageClient.GetObjectAsync(_settings.BucketName, objectName);
            if (res == null || res.TimeDeleted != null)
            {
                return null;
            }
            var decodedHash = Convert.FromBase64String(res.Md5Hash);
            return decodedHash.BytesToString();
        }
        catch
        {
            return null;
        }
    }

    public virtual async Task<bool> CheckFileExistenceInBucket(IStorageFile file)
    {
        try
        {
            var objectName = file.ObjectName;
            var res = await _storageClient.GetObjectAsync(file.BucketName, objectName);
            return res != null && res.TimeDeleted == null;
        }
        catch
        {
            return false;
        }
    }


}
