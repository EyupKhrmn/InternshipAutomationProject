namespace InternshipAutomation.Security.Token;

public interface IDecodeTokenService
{
    public string GetUsernameFromToken(string token);

    public string GetRoleFromToken(string token);
}