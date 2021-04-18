using System;
using System.Security.Claims;
using Onion.Domain.Users;

namespace Onion.Services.Users
{
    public partial class UserService : IUserService
    {


        public void CreateNewUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool IsUserLoggedIn(ClaimsPrincipal user) => user.Identity.IsAuthenticated;


    }
}
