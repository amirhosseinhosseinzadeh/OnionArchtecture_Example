using Microsoft.AspNetCore.Http;
using Onion.Libraries.Domain.Security;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Onion.Services.Security
{
    public interface ISecurityService
    {
        ClaimsPrincipal PrepareCookieCliamnsPrincipal(string userName, string authenticationType, string role = null);

        Task LoginUserByClaimPrincipal(ClaimsPrincipal claimsPrincipal,  HttpContext httpContext);

        Task LogoutUser(HttpContext httpContext);

        Task InsertRole(Role role);

        IQueryable<Role> GetAllRoles();

        Task CreateDefaultRoles();
    }
}
