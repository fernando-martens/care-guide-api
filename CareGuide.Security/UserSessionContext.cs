using CareGuide.Security.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CareGuide.Security
{
    public class UserSessionContext : IUserSessionContext
    {
        public Guid UserId { get; }
        public Guid PersonId { get; }
        public string Email { get; }

        public UserSessionContext(IHttpContextAccessor httpContextAccessor, IJwtService jwtService)
        {
            var httpContext = httpContextAccessor.HttpContext;

            if (httpContext == null)
                throw new UnauthorizedAccessException("No HttpContext available.");

            if (!httpContext.Request.Headers.TryGetValue("Authorization", out var headerAuth))
                throw new UnauthorizedAccessException("Authorization header not found.");

            var token = jwtService.ValidateToken(headerAuth.ToString().Replace("Bearer ", ""));

            if (token == null)
                throw new UnauthorizedAccessException("Invalid or expired token.");

            UserId = GetGuidClaim(token.Claims, "sub", "UserId");
            PersonId = GetGuidClaim(token.Claims, "personId", "PersonId");
            Email = GetStringClaim(token.Claims, "email");
        }

        private static Guid GetGuidClaim(IEnumerable<Claim> claims, string type, string name)
        {
            var value = claims.FirstOrDefault(c => c.Type == type)?.Value;

            if (value == null)
                throw new UnauthorizedAccessException($"{name} claim ('{type}') not found in token.");

            if (!Guid.TryParse(value, out var guid))
                throw new UnauthorizedAccessException($"{name} claim ('{type}') is not a valid GUID: {value}");

            return guid;
        }

        private static string GetStringClaim(IEnumerable<Claim> claims, string type)
        {
            return claims.FirstOrDefault(c => c.Type == type)?.Value ?? string.Empty;
        }
    }
}
