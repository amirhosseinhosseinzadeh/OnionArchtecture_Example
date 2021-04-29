using Microsoft.AspNetCore.Mvc;
using System;

namespace Onion.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return Content("Yore in admin area");
        }
    }
}