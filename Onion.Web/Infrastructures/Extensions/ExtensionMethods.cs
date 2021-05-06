using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace Onion.Web.Infrastructures.Extensions
{
    public static class ExtensionMethods
    {
        public static AuthenticationBuilder DefaultCookieBuilder(this AuthenticationBuilder builder)
        {
            return builder.AddCookie(auth =>
            {
                auth.LoginPath = "/Login";
                auth.ReturnUrlParameter = "/returnUrl";
                auth.AccessDeniedPath = "/AccessDenied";
                auth.ExpireTimeSpan = TimeSpan.FromDays(9);
                auth.LogoutPath = "/Logout";
                auth.Validate(CookieAuthenticationDefaults.AuthenticationScheme);
                auth.Cookie = new CookieBuilder()
                {
                    Name = Constants.DefaultCookieSchemaName
                };
            });
        }

        public static string GetReturnUrl(this string queryString)
        {
            queryString = queryString.ToLower();
            const string Pattern = "returnurl=";
            if (!queryString.Contains(Pattern, StringComparison.InvariantCultureIgnoreCase))
                return string.Empty;

            var len = queryString.LastIndexOf(Pattern, StringComparison.InvariantCultureIgnoreCase);
            var returnUrl = queryString.Substring(len + Pattern.Length);
            if (returnUrl.Contains("&", StringComparison.InvariantCultureIgnoreCase))
            {
                len = returnUrl.IndexOf('&');
                returnUrl = returnUrl.Substring(0, len);
            }
            return returnUrl;
        }

        public static void PublishRoute(this IEndpointRouteBuilder endpointRouteBuilder,
            string name,
            string url,
            string controller,
            string action,
            bool isAdminRoute = false
            )
        {
            if (isAdminRoute)
            {
                endpointRouteBuilder.MapControllerRoute(name,
                    url,
                    new
                    {
                        controller,
                        action,
                        area = Constants.AdminArea
                    });
            }
            else
            {
                endpointRouteBuilder.MapControllerRoute(name,
                    url,
                    new
                    {
                        controller,
                        action
                    });
            }
        }
    }
}
