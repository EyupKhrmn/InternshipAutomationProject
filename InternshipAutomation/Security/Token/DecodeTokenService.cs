using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Azure.Identity;

namespace InternshipAutomation.Security.Token;

public class DecodeTokenService : IDecodeTokenService
{
    public string GetUsernameFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

        if (jsonToken is not null)
        {
            var username = jsonToken.Claims
                .FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

            return username;
        }
        
        return "Token Boş Veya hatalı";
    }
    
    
    public string GetRoleFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

        if (jsonToken is not null)
        {
            var role = jsonToken.Claims
                .FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/role").Value;

            return role;
        }
        
        return "Token Boş Veya hatalı";
    }
}