namespace CareGuide.Security.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, string email);
        bool ValidateToken(string token);
    }
}