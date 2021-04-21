using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using Onion.Libraries.Domain.Security;
using Onion.Data.Infrastructures;
using System;

namespace Onion.Services.Security
{
    public partial class SecurityService : ISecurityService
    {

        private readonly IRepository<Role> _roleRepository;

        public SecurityService(IRepository<Role> roleRepository)
        {
            this._roleRepository = roleRepository;
        }


        public async Task CreateDefaultRoles()
        {
            var roles = new List<Role>()
            {
                new Role("Admin"),
                new Role("User"),
                new Role("Vendor")
            };

            await _roleRepository.InsertManyAsync(roles);
        }

        public IQueryable<Role> GetAllRoles()
        {
            return _roleRepository.Table;
        }

        public async Task InsertRole(Role role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            await _roleRepository.InsertAsync(role);
        }

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
