using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Onion.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Onion.Services.Users;
using Onion.Services.Security;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Onion.Domain.Users;

namespace Onion.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly ISecurityService _securityService;

        public HomeController(ILogger<HomeController> logger,
                              IUserService userService,
                              ISecurityService securityService
                              )
        {
            this._logger = logger;
            this._userService = userService;
            this._securityService = securityService;
        }

        #region Utilities
        void PrepareAvailableReoles(UserModel model)
        {
            var allRoles = _securityService.GetAllRoles();

            model.AvailableRoles = allRoles.Select(ar => new SelectListItem()
            {
                Text = ar.RoleName,
                Value = ar.Id.ToString()
            }).ToList();
        }
        #endregion


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("add-user")]
        public IActionResult CreateUser()
        {
            var model = new UserModel();
            PrepareAvailableReoles(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateUser(UserModel model)
        {
            if (string.IsNullOrWhiteSpace(model.UserName))
                ModelState.AddModelError(nameof(model.UserName), "Username is required");

            if (string.IsNullOrWhiteSpace(model.Password))
                ModelState.AddModelError(nameof(model.Password), "Password is not qualified");

            if (model.Password.Equals(model.RepeatPassword, StringComparison.InvariantCultureIgnoreCase))
                ModelState.AddModelError(nameof(model.RepeatPassword), "Password's are not equal");

            if (string.IsNullOrWhiteSpace(model.Email))
                ModelState.AddModelError(nameof(model.Email), "Email is required");

            if (!ModelState.IsValid)
                return RedirectToAction(nameof(CreateUser));

            var user = new User()
            {
                EmailAddress = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber
            };

            _userService.CreateNewUser(user);

            return Redirect("/");
        }

        public IActionResult CreateDefaults()
        {
            _securityService.CreateDefaultRoles();
            _userService.CreateNewUser(new User()
            {
                EmailAddress = "my@emailaddress.com",
                PhoneNumber = 12345689,
                UserName = "Jhon smith"
            });
            return Redirect("/");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Secret", Name = "Secret")]
        public IActionResult Secret()
        {
            return View();
        }
    }
}
