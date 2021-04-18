using System.Security.Claims;
using Onion.Domain.Users;

namespace Onion.Services.Users
{
    public interface IUserService
    {
        bool IsUserLoggedIn(ClaimsPrincipal user);

        void CreateNewUser(User user);
    }
}
