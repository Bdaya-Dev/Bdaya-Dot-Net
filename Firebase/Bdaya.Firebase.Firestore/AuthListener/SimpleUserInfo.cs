namespace Bdaya.Firebase.Firestore;

public record SimpleUserInfo(string Uid,
    string DisplayName,
    string Email,
    string PhoneNumber,
    string PhotoUrl,
    string ProviderId) : IUserInfo;