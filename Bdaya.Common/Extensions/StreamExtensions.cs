using System.Security.Cryptography;

namespace System;

public static class StreamExtensions
{
    /// <summary>
    /// Compute the file's MD5 hash
    /// </summary>
    /// <param name="stream">any type of stream</param>  
    /// <returns>array of bytes</returns>
    public static async Task<byte[]> GetHashMd5(this Stream stream, CancellationToken cancellationToken = default)
    {
        var originalSeek = stream.Position;
        using var hasher = MD5.Create();
        var res = await hasher.ComputeHashAsync(stream, cancellationToken);
        if (stream.CanSeek)
            stream.Seek(originalSeek, SeekOrigin.Begin);
        return res;
    }

    public static string BytesToString(this byte[] bytes)
    {
        return Convert.ToBase64String(bytes).ToUpperInvariant();
    }

    public static string RemoveInvalidChars(this string filename) => string.Concat(filename.Split(Path.GetInvalidFileNameChars()));



}
