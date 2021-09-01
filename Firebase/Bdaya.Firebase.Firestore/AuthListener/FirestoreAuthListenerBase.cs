


namespace Bdaya.Firebase.Firestore;

public abstract class FirestoreAuthListenerBase
{
    protected readonly FirestoreDb _db;
    protected readonly AuthListenerSettings _settings;

    public FirestoreAuthListenerBase(FirestoreDb client, AuthListenerSettings settings)
    {
        _db = client;
        _settings = settings;
    }

    public abstract Task HandleUsersCreated(IReadOnlyDictionary<string, (ExtraUserRecord parsed, DocumentSnapshot snap)> dict);

    public virtual FirestoreChangeListener InitUsersListen()
    {
        var res = _db.Collection(_settings.UsersCollectionName).Listen(async (snapshot, cancellationToken) =>
        {
            var parsedMap = new Dictionary<string, (ExtraUserRecord parsed, DocumentSnapshot snap)>();
            foreach (var item in snapshot.Documents)
            {
                var parsed = item.ConvertTo<ExtraUserRecord>();
                parsedMap[item.Id] = (parsed, item);
            }
            await HandleUsersCreated(parsedMap);
            var batch = _db.StartBatch();
            foreach (var item in parsedMap)
            {
                batch.Delete(item.Value.snap.Reference);
            }
            await batch.CommitAsync(cancellationToken);

        });
        return res;
    }
}
