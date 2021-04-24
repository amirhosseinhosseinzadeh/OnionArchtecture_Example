using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Onion.Services.Security;
using Onion.Services.Users;
using Onion.Web.Infrastructures.Extensions;
using Onion.Web.Models;
using System.Net;
using System.Threading.Tasks;

namespace Onion.Web.Controllers.Bases
{
    public class BasePublicController : Controller
    {

        #region Fields

        private readonly ISecurityService _securityService;
        private readonly IUserService _userService;

        #endregion

        #region Ctor

        public BasePublicController(ISecurityService securityService,
                                    IUserService userService
                                    )
        {
            this._securityService = securityService;
            this._userService = userService;
        }

        #endregion


        [HttpGet("Login", Name = "Login")]
        public IActionResult Login()
        {
            var returnUrl = WebUtility.UrlDecode(Request.QueryString.Value);

            if (!string.IsNullOrWhiteSpace(returnUrl))
                ViewData["returnUrl"] = returnUrl.GetReturnUrl();

            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            var currentUser = await _userService.GetUserByUserNameOrPasswordAsync(model.EmailOrUserName, model.Password);

            if (currentUser == null)
                ModelState.AddModelError(nameof(model.EmailOrUserName), "There is no refistered user with username or email or this password");

            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Login));

            await _userService.loginUserAsync(currentUser,
                CookieAuthenticationDefaults.AuthenticationScheme,
                HttpContext);

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);

            return Redirect("/");
        }

        [HttpGet("/LogOut", Name = "LogOut")]
        public async Task<IActionResult> LogOut()
        {
            if (_userService.IsUserLoggedIn(User))
                await _securityService.LogoutUser(HttpContext);

            return Redirect("/");
        }
    }
}
