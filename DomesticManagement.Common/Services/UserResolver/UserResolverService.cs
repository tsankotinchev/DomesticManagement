using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;


namespace DomesticManagement.Common.Services.UserResolver
{

    public class UserResolverService : IUserResolverService
    {
        private readonly IHttpContextAccessor _context;

        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public ClaimsPrincipal GetUser()
        {
            return _context.HttpContext?.User;
        }

        public Guid? GetUserId()
        {
            var userId = _context.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return userId != null ? (Guid?)new Guid(userId) : null;
        }

        public string GetUserName()
        {
            return _context.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub).Value;
        }

        public bool IsUserLoggedIn()
        {
            return _context.HttpContext.User.Identities.Any(x => x.IsAuthenticated);
        }
    }
}
