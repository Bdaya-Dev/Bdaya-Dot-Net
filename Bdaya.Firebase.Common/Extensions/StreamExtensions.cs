using System.Security.Cryptography;
using System.Text;

namespace System;

public static class StreamExtensions
{
    /// <summary>
    /// Compute the file's MD5 hash
    /// </summary>
    /// <param name="stream">any type of stream</param>  
    /// <returns>array of bytes</returns>
    public static async Task<byte[]> GetHashMd5(this Stream stream)
    {
        using var hasher = MD5.Create();
        var res = await hasher.ComputeHashAsync(stream);
        return res;
    }

    public static string BytesToString(this byte[] bytes)
    {
        var sb = new StringBuilder();

        foreach (var b in bytes)
            sb.Append(b.ToString("x2"));
        return sb.ToString();
    }

    public static string RemoveInvalidChars(this string filename) => string.Concat(filename.Split(Path.GetInvalidFileNameChars()));



}
