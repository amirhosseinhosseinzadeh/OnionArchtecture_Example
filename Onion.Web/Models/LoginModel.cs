namespace Onion.Web.Models
{
    public class LoginModel
    {
        public string EmailOrUserName { get; set; }

        public string Password { get; set; }

        public bool RmemberMe { get; set; }
    }
}
