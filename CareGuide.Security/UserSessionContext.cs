using CareGuide.Security.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CareGuide.Security
{
    public class UserSessionContext: IUserSessionContext
    {
        public readonly Guid? _userId;
        public Guid? UserId => _userId;

        public UserSessionContext(IHttpContextAccessor httpContextAccessor)
        {
            Claim? userIdClaim = httpContextAccessor.HttpContext?.User.Claims?.ToList().Find((e) => e.Type == JwtRegisteredClaimNames.Sub);  
            
            if(userIdClaim != null) 
                _userId = new Guid(userIdClaim.Value);
        }

    }
}
