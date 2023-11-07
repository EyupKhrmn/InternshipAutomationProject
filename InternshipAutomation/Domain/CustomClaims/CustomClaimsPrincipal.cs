using System.Security.Claims;

namespace InternshipAutomation.Domain.CustomClaims;

public class CustomClaimsPrincipal : ClaimsPrincipal
{
    
    public CustomClaimsPrincipal(ClaimsIdentity identity)
        : base(identity)
    {
    }
}