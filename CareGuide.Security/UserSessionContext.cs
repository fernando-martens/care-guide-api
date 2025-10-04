using CareGuide.Security.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CareGuide.Security
{
    public class UserSessionContext : IUserSessionContext
    {
        public Guid UserId { get; private set; }
        public Guid PersonId { get; private set; }
        public string Email { get; private set; } = string.Empty;

        public UserSessionContext(IHttpContextAccessor httpContextAccessor, IJwtService jwtService)
        {
            var httpContext = httpContextAccessor.HttpContext;

            if (httpContext == null)
                return;

            if (!httpContext.Request.Headers.TryGetValue("Authorization", out var headerAuth))
                return;

            var tokenString = headerAuth.ToString().Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase);
            var token = jwtService.ValidateToken(tokenString);

            if (token == null)
                return;

            UserId = GetGuidClaim(token.Claims, "sub");
            PersonId = GetGuidClaim(token.Claims, "personId");
            Email = GetStringClaim(token.Claims, "email");
        }

        private static Guid GetGuidClaim(IEnumerable<Claim> claims, string type)
        {
            var value = claims.FirstOrDefault(c => c.Type == type)?.Value;
            return Guid.TryParse(value, out var guid) ? guid : Guid.Empty;
        }

        private static string GetStringClaim(IEnumerable<Claim> claims, string type)
        {
            return claims.FirstOrDefault(c => c.Type == type)?.Value ?? string.Empty;
        }
    }
}
