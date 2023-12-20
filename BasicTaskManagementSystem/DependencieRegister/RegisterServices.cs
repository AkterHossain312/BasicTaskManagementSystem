using Application.Implementation;
using Application.Interface;

namespace WebApi.DependencieRegister
{
    public static class RegisterServices
    {
        public static IServiceCollection AddServicesDependency(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            

            return services;
        }
    }
}
