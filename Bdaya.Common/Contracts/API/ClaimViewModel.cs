
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Bdaya.Responses;

public class ClaimViewModel
{
    public string Type { get; set; } = default!;
    public string Value { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public static List<ClaimViewModel> GetClaimViewModels(IEnumerable<Claim> claims)
    {
        return claims.Select((Claim x) => new ClaimViewModel()
        {
            Issuer = x.Issuer,
            Value = x.Value,
            Type = x.Type
        }).ToList();
    }

}
