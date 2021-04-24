using System.Security.Claims;
using Onion.Domain.Users;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Onion.Services.Users
{
    public interface IUserService
    {
        bool IsUserLoggedIn(ClaimsPrincipal user);

        Task CreateNewUser(User user);

        Task<User> GetUserByUserNameOrPasswordAsync(string userNameOrEmail, string password);

        Task loginUserAsync(User user, string authenticationSchem, HttpContext httpContext);
    }
}
