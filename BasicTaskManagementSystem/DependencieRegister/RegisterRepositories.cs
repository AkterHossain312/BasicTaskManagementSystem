using Infrastructure.Interface;
using Infrastructure.Repositories;

namespace WebApi.DependencieRegister
{
    public static class RegisterRepositories
    {
        public static IServiceCollection AddRepositoriesDependency(this IServiceCollection services)
        {
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            return services;
        }
    }
}
