using System.IdentityModel.Tokens.Jwt;

namespace CareGuide.Security.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, string email);
        JwtSecurityToken? ValidateToken(string token);
    }
}