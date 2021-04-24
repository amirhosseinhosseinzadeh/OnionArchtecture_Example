using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using Onion.Libraries.Domain.Security;
using Onion.Data.Infrastructures;
using System;
using System.Text;
using System.Security.Cryptography;

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

        public async Task CreateMany(IList<Role> roles)
        {
            await _roleRepository.InsertManyAsync(roles);
        }

        public IQueryable<Role> GetAllRoles()
        {
            return _roleRepository.Table;
        }

        public string HashString(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new Exception("System won't hash an empty string");

            byte[] data = Encoding.UTF8.GetBytes(str);
            using var md5 = MD5.Create();
            var hashData = md5.ComputeHash(data);
            var sb = new StringBuilder();
            md5.Dispose();

            for (var i = 0; i < hashData.Length; i++)
            {
                sb.Append(hashData[i].ToString("X2"));
            }

            return sb.ToString();
        }

        public string HashPassword(string password, string key)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(key))
                throw new Exception("password or user key is null or empty");

            var hash = HashString(string.Concat(password, key));
            if (string.IsNullOrEmpty(hash))
                throw new Exception("hash is empty or null");
            return hash;
        }

        public string PrepareUserKey()
        {
            string userKey = Guid.NewGuid().ToString();
            userKey = userKey.Trim(new char[] { '-', ' ' });
            userKey = userKey.Take(5).ToString();
            return userKey;
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

        public ClaimsPrincipal PrepareCookieCliamnsPrincipal(string userName, string emailAddress, string authenticationType, string role = null)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userName));
            claims.Add(new Claim(ClaimTypes.Email,emailAddress));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userName));
            claims.Add(new Claim(ClaimTypes.Role, role ?? "User"));
            var claimsIdentity = new ClaimsIdentity(claims, authenticationType);
            var claimPrincipal = new ClaimsPrincipal(claimsIdentity);

            return claimPrincipal;
        }
    }
}
