using Application.Helper;
using Domain.Helpers;
using Domain.Interface;
using Infrastructure.Constant;
using Infrastructure.Interface;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using WebApi.Extensions;
using WebApi.Seeders;

namespace WebApi.DependencieRegister
{
    public static class BaseRegisterDependencies
    {
        public static IServiceCollection AddAllRegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServicesDependency();
            services.AddRepositoriesDependency();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAuthorizationHandler, CustomAuthorizationExtension>();
            services.AddSingleton<ICurrentUser, CurrentUserService>();
            services.AddScoped<DatabaseSeeder>();
            services.AddScoped<SeedIdentityHelper>();
            services.Configure<SeedDataFilesConfiguration>(configuration.GetSection(AppConstants.SeedDataFileConfig));

            return services;
        }
    }
}