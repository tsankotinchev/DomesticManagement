using System;
using System.Security.Claims;

namespace DomesticManagement.Common.Services.UserResolver
{
    public interface IUserResolverService
    {
        ClaimsPrincipal GetUser();
        Guid? GetUserId();
        string GetUserName();
        bool IsUserLoggedIn();
    }
}
