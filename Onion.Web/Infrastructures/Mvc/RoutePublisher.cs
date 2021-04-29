using System;
using Microsoft.AspNetCore.Routing;

namespace Onion.Web.Infrastructures.Mvc
{
    public class RoutePublisher : IRoutePublisher
    {
        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            var routeProvider = typeof(RouteProvider);
            var instance = (RouteProvider)Activator.CreateInstance(routeProvider);
            instance.RegisterRoutes(endpointRouteBuilder);
        }
    }
}
