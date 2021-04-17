using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Onion.Services.User
{
    public interface IUserService
    {
        bool IsUserLoggedIn(ClaimsPrincipal user);
    }
}
