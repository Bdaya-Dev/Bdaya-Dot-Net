namespace Bdaya.Responses;

public class TokenOnlyBagViewModel<TUserViewModel>
{
    public string Token { get; set; } = default!;
    public TUserViewModel User { get; set; } = default!;
    public List<ClaimViewModel> Claims { get; set; } = new();
}
