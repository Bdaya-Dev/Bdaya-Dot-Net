namespace Bdaya.Models.Files;

/// <summary>
/// A record representing a file at the client side
/// </summary>
/// <param name="Md5Hash"></param>
/// <param name="FileName"></param>
public record FirebaseStorageClientFileProposal(string Md5Hash, string FileName);
