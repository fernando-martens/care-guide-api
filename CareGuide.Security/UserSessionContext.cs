using CareGuide.Security.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;

namespace CareGuide.Security
{
    public class UserSessionContext: IUserSessionContext
    {
        public readonly Guid? _userId;
        public Guid UserId { 
            get { 
                return _userId ?? throw new UnauthorizedAccessException();
            } 
        }

        public UserSessionContext(IHttpContextAccessor httpContextAccessor, IJwtService jwtService)
        {
            if (httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("Authorization", out StringValues headerAuth) == true)
            {
                JwtSecurityToken? token = jwtService.ValidateToken(headerAuth.ToString().Replace("Bearer ", ""));

                if (token != null)
                {
                    string? claimValue = token.Claims.ToList().Find((e) => e.Type == "sub")?.Value;
                    if(claimValue != null) _userId = Guid.Parse(claimValue);
                }

            }

        }

    }
}
