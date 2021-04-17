using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Onion.Services.Security
{
    public interface ISecurityService
    {
        ClaimsPrincipal PrepareCookieCliamnsPrincipal(string userName, string authenticationType, string role = null);

        Task LoginUserByClaimPrincipal(ClaimsPrincipal claimsPrincipal,  HttpContext httpContext);

        Task LogoutUser(HttpContext httpContext);
    }
}
