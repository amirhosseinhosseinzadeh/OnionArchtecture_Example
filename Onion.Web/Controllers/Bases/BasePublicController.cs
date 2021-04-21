using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Onion.Services.Security;
using Onion.Services.Users;
using Onion.Web.Infrastructures.Extensions;
using Onion.Web.Models;
using System;
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
            if (!model.EmailOrUserName.Equals("test1", StringComparison.InvariantCultureIgnoreCase) &&
                !model.Password.Equals("1", StringComparison.InvariantCultureIgnoreCase))
                return RedirectToAction(nameof(Login));

            var claimsPrincipal = _securityService.PrepareCookieCliamnsPrincipal(model.EmailOrUserName, CookieAuthenticationDefaults.AuthenticationScheme);

            await _securityService.LoginUserByClaimPrincipal(claimsPrincipal, HttpContext);

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
