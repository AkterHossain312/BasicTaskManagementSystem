using Framework.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Dependency
{
    public static class ServiceConfigurations
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IDbContextService, DbContextService>();
            return services;
        }

    }
}