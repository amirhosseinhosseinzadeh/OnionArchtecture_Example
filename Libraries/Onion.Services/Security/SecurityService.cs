using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Onion.Services.Security
{
    public partial class SecurityService : ISecurityService
    {
        public async Task LoginUserByClaimPrincipal(ClaimsPrincipal claimsPrincipal, HttpContext httpContext)
        {
            await httpContext.SignInAsync(claimsPrincipal);
        }

        public async Task LogoutUser(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }

        public ClaimsPrincipal PrepareCookieCliamnsPrincipal(string userName, string authenticationType, string role = null)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userName));
            claims.Add(new Claim(ClaimTypes.Role, role ?? "User"));
            var claimsIdentity = new ClaimsIdentity(claims, authenticationType);
            var claimPrincipal = new ClaimsPrincipal(claimsIdentity);

            return claimPrincipal;
        }
    }
}
