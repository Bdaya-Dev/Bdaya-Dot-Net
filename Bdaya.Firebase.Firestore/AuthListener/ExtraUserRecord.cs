namespace Bdaya.Firebase.Firestore;

public record ExtraUserRecord(
    string Uid,
    string DisplayName,
    string Email,
    string PhoneNumber,
    string PhotoUrl,
    string ProviderId,
    bool EmailVerified,
    bool Disabled,
    List<SimpleUserInfo> ProviderData) : SimpleUserInfo(
        Uid: Uid,
        DisplayName: DisplayName,
        Email: Email,
        PhoneNumber: PhoneNumber,
        PhotoUrl: PhotoUrl,
        ProviderId: ProviderId);
