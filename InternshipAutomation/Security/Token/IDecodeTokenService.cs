using InternshipAutomation.Domain.User;

namespace InternshipAutomation.Security.Token;

public interface IDecodeTokenService
{
    public Task<User> GetUsernameFromToken();

    public string GetRoleFromToken(string token);
}