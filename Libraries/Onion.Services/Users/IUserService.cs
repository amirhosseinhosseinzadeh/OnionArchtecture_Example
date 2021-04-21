using System.Security.Claims;
using Onion.Domain.Users;
using System.Threading.Tasks;

namespace Onion.Services.Users
{
    public interface IUserService
    {
        bool IsUserLoggedIn(ClaimsPrincipal user);

        Task CreateNewUser(User user);


    }
}
