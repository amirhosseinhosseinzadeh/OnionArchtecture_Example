using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Onion.Web.Infrastructures.ActionFilters
{
    public class MapArgument : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.QueryString.HasValue)
            {
                var argument = context.ActionDescriptor;
                
            }
        }
    }
}
