using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Onion.Web.Models
{
    public class UserModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public long PhoneNumber { get; set; }

        public string Password { get; set; }

        public string RepeatPassword { get; set; }

        public IList<SelectListItem> AvailableRoles { get; set; }
    }
}
