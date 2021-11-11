namespace Bdaya.Models.Files;

public interface IFirebaseStorageFile
{
    /// <summary>
    /// [firebaseProjectId].appspot.com
    /// </summary>
    string BucketName { get; }

    /// <summary>
    /// The full path of the object on firebase storage including the filename
    /// </summary>
    string ObjectName { get; }
}
