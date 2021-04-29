using System.Linq;
using System;
using System.Security.Claims;
using Onion.Domain.Users;
using System.Threading.Tasks;
using Onion.Data.Infrastructures;
using Onion.Services.Security;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Onion.Services.Users
{
    public partial class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly ISecurityService _securityService;

        public UserService(IRepository<User> userRepository,
                           ISecurityService securityService
                           )
        {
            this._userRepository = userRepository;
            this._securityService = securityService;
        }

        public async Task CreateNewUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var userKey = _securityService.PrepareUserKey();
            var hashedPassword = _securityService.HashPassword(user.Password, userKey);

            user.Password = hashedPassword;
            user.Key = userKey;

            await _userRepository.InsertAsync(user);
        }

        public async Task<User> GetUserByUserNameOrPasswordAsync(string userNameOrEmail, string password)
        {
            var users = _userRepository.Table;

            var usersMatchedWithUserName = users.Where(user =>
                user.UserName.Equals(userNameOrEmail, StringComparison.InvariantCultureIgnoreCase));
            if (!usersMatchedWithUserName.Any())
            {
                var userMatchedWithEmailAddress = users.SingleOrDefault(user =>
                    user.EmailAddress.Equals(userNameOrEmail, StringComparison.InvariantCultureIgnoreCase));
                var currentHashedPassword = _securityService.HashPassword(password, userMatchedWithEmailAddress.Key);
                if (userMatchedWithEmailAddress != null)
                {
                    if (userMatchedWithEmailAddress.Password.Equals(currentHashedPassword, StringComparison.InvariantCultureIgnoreCase))
                        return userMatchedWithEmailAddress;
                }
                return null;
            }

            if (usersMatchedWithUserName.Count() > 1)
            {
                var comparedUsersTaskList = new List<Task<User>>();
                foreach (var userMatched in usersMatchedWithUserName)
                {
                    comparedUsersTaskList.Add(Task.Run<User>(() =>
                    {
                        var hashedPassword = _securityService.HashPassword(password, userMatched.Key);
                        if (userMatched.Password.Equals(hashedPassword, StringComparison.InvariantCultureIgnoreCase))
                        {
                            return userMatched;
                        }
                        return null;
                    }));
                }
                var comparedUserList = (IList<User>)await Task.WhenAll(comparedUsersTaskList);
                switch (comparedUserList.Count())
                {
                    case 1:
                        return comparedUserList.First();
                        break;

                    case 0:
                        return null;
                        break;

                    default:
                        var sb = new StringBuilder();
                        sb.AppendLine("{");
                        int counter = 1;
                        foreach (var brokedUser in comparedUserList)
                        {
                            sb.AppendLine(string.Format("{0}={UserId = {1} , UserName = {2} }", counter, brokedUser.Id, brokedUser.UserName));
                            counter++;
                        }
                        sb.AppendLine("}");
                        throw new Exception($"There is some user with equal user name and password :{sb}");
                        break;
                }
            }
            var currentUser = usersMatchedWithUserName.First();
            var hashedPassword = _securityService.HashPassword(password, currentUser.Key);

            return currentUser.Password
                .Equals(hashedPassword, StringComparison.InvariantCultureIgnoreCase)
                    ? currentUser
                    : null;
        }

        public async Task loginUserAsync(User user, string authenticationSchem, HttpContext httpContext)
        {
            var claimPrincipal = _securityService.PrepareCookieCliamnsPrincipal(user.UserName, user.EmailAddress, authenticationSchem);
            await _securityService.LoginUserByClaimPrincipal(claimPrincipal, httpContext);
        }

        public bool IsUserLoggedIn(ClaimsPrincipal user) => user.Identity.IsAuthenticated;

    }
}
