namespace Bdaya.Responses;

public class TokenBagViewModel<TUserViewModel>
{
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public DateTime Expiration { get; set; }
    public TUserViewModel User { get; set; } = default!;
    public List<ClaimViewModel> Claims { get; set; } = new();
}
