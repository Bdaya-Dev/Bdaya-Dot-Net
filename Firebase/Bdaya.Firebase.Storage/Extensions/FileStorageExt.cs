using Bdaya.Models.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System.Collections.ObjectModel;

namespace Bdaya.Firebase.Storage.Extensions;

public static class FileStorageExt
{
    /// <summary>
    /// Parses basic information about the input files
    /// </summary>
    /// <param name="files"></param>
    /// <param name="getContentType">A custom function to get the content type from IFormFile</param>
    /// <param name="getFileName">A custom function to get the file name from IFormFile</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Mapping between file md5 hash and file stream
    /// Don't forget to dispose the stream after you are done with it
    /// </returns>
    /// 
    public static async Task<IReadOnlyDictionary<string, (BasicFileRecord info, Stream file)>> ToBasicFile(this IReadOnlyList<IFormFile> files, Func<IFormFile, string>? getFileName = null, Func<IFormFile, string>? getContentType = null, CancellationToken cancellationToken = default)
    {
        var result = new Dictionary<string, (BasicFileRecord info, Stream file)>(files.Count);
        var provider = new FileExtensionContentTypeProvider();
        for (int i = 0; i < files.Count; i++)
        {
            var file = files[i];

            var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream, cancellationToken);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var hash = (await memoryStream.GetHashMd5(cancellationToken: cancellationToken)).BytesToBase64String();
            if (result.ContainsKey(hash))
            {
                continue;
            }

            var originalName = file.FileName.RemoveInvalidChars();


            string? contentType;
            if (getContentType is not null)
            {
                contentType = getContentType(file);
            }
            else
            {
                contentType = file.ContentType;
                if (string.IsNullOrWhiteSpace(contentType))
                {
                    if (provider.TryGetContentType(file.FileName, out var contentTypeRes))
                    {
                        contentType = contentTypeRes;
                    }
                }
            }



            string name;
            if (getFileName is not null)
            {
                name = getFileName(file);
            }
            else
            {
                var nameWithExt = originalName;
                var ext = Path.GetExtension(nameWithExt);
                var nameWithoutExt = Path.GetFileNameWithoutExtension(nameWithExt);

                name = $"{nameWithoutExt}-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}{ext}";
            }


            result[hash] = (new(Md5Hash: hash, OriginalFileName: originalName, FileName: name, ContentType: contentType, FileSizeInBytes: file.Length), memoryStream);
        }
        return new ReadOnlyDictionary<string, (BasicFileRecord info, Stream file)>(result);
    }
}
