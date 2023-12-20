using Infrastructure;
using Microsoft.EntityFrameworkCore;
using WebApi.Constants;

namespace WebApi.Extensions
{
    public static class DbContextExtension
    {
        private static readonly ILoggerFactory _consoleLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter((category, level) =>
                    category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information)
                .AddConsole();
        });
        public static IServiceCollection RegisterDbContext(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<TaskManagementDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseLoggerFactory(_consoleLoggerFactory)
                     .EnableSensitiveDataLogging()
                     .UseSqlServer(configuration.GetConnectionString(ConfigurationConstants.ConnectionStringName));
            });

            return service;
        }

        public static void EnsureDatabaseExists(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            var context = serviceScope.ServiceProvider.GetService<TaskManagementDbContext>();

            context.Database.Migrate();
        }
    }
}