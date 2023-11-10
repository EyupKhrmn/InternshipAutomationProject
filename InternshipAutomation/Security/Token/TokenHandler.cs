using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InternshipAutomation.Security.Token;

public class TokenHandler
{
    public static Token CreateToken(IConfiguration configuration,string UserName,string Password,IList<string> Role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, UserName),
            new Claim(ClaimTypes.Role, Role[0])
        };
        Token token = new();

        SymmetricSecurityKey securityKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));

        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        token.Expiration = DateTime.Now.AddDays(Convert.ToInt16(configuration["Token:Expiration"]));
        
        JwtSecurityToken jwtToken = new(
            issuer: configuration["Token:Issuer"],
            audience: configuration["Token:Audience"],
            expires: token.Expiration,
            notBefore: DateTime.Now,
            signingCredentials: credentials,
            claims: claims
        );

        JwtSecurityTokenHandler tokenHandler = new();

        token.AccessToken = tokenHandler.WriteToken(jwtToken);

        byte[] numbers = new byte[32];
        using RandomNumberGenerator random = RandomNumberGenerator.Create();
        random.GetBytes(numbers);
        token.RefreshToken = Convert.ToBase64String(numbers);

        return token;
    }
}