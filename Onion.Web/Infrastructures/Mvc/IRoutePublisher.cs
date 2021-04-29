using Microsoft.AspNetCore.Routing;

namespace Onion.Web.Infrastructures.Mvc
{
    public interface IRoutePublisher
    {
        void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder);
    }
}
