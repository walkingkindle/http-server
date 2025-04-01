using codecrafters_http_server.src.Application.Interfaces;
using codecrafters_http_server.src.Application.Services;
using codecrafters_http_server.src.Application.Services.Routes.GET;
using codecrafters_http_server.src.Application.Services.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace codecrafters_http_server.src.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddTransient<IHttpRouteHandler, DefaultRouteHandler>();

            services.AddTransient<IHttpRouteHandler, EchoRouteHandler>();

            services.AddTransient<IHttpRouteHandler, FilesRouteHandler>();

            services.AddTransient<IHttpRouteHandler, NotFoundRouteHandler>();

            services.AddTransient<IHttpRouteHandler, UserAgentRouteHandler>();

            services.AddTransient<IHttpRouteHandler, Services.Routes.POST.FilesRouteHandler>();

            services.AddTransient<IRouteManagerService, RouteManagerService>();

            return services; 
        }
    }
}
