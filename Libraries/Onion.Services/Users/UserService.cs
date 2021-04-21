using System;
using System.Security.Claims;
using Onion.Domain.Users;
using System.Threading.Tasks;
using Onion.Data.Infrastructures;

namespace Onion.Services.Users
{
    public partial class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task CreateNewUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await _userRepository.InsertAsync(user);
        }

        public bool IsUserLoggedIn(ClaimsPrincipal user) => user.Identity.IsAuthenticated;

    }
}
