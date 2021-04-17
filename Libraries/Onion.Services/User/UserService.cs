using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Onion.Services.User
{
    public partial class UserService : IUserService
    {
        public bool IsUserLoggedIn(ClaimsPrincipal user) => user.Identity.IsAuthenticated;


    }
}
