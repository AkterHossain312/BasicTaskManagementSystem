using Application.Implementation;
using Application.Interface;

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
