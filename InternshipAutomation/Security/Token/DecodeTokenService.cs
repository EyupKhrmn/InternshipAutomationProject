using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Azure.Identity;
using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.User;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomation.Security.Token;

public class DecodeTokenService : IDecodeTokenService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IGeneralRepository _generalRepository;

    public DecodeTokenService(IHttpContextAccessor contextAccessor, IGeneralRepository generalRepository)
    {
        _contextAccessor = contextAccessor;
        _generalRepository = generalRepository;
    }

    public async Task<User> GetUsernameFromToken()
    {
        var token = _contextAccessor.HttpContext.Request.Cookies["AuthToken"];
        
        var handler = new JwtSecurityTokenHandler();

        if (handler.ReadToken(token) is JwtSecurityToken jsonToken)
        {
            var username = jsonToken.Claims
                .FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

            var user = await _generalRepository.Query<User>()
                .FirstOrDefaultAsync(_=>_.UserName == username);

            return user;
        }
        
        return new User();
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