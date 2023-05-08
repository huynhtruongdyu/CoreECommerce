using CEC.Application.Abstractions.Contexts;
using CEC.Application.Abstractions.Services;
using CEC.Application.Abstractions.UnitOfWork;
using CEC.Infrastructure.Contexts;
using CEC.Infrastructure.Repositories;
using CEC.Infrastructure.Services;
using CEC.Shared.Constants;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CEC.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        //Configure database
        public static void AddAndMigrateTenantDatabases(this IServiceCollection services)
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var databaseProviderService = scope.ServiceProvider.GetRequiredService<IDatabaseProviderService>();

            var connectionString = databaseProviderService.GetConnectionString();
            var provider = databaseProviderService.GetProvider();

            AddDbContext(provider, connectionString);

            MigrateDatabase();

            #region Local methods

            void AddDbContext(string provider, string connectionString)
            {
                switch (provider)
                {
                    case DatabaseProviderConstant.MSSQL:
                        services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));
                        break;

                    default:
                        services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlite(connectionString));
                        break;
                }
            }

            void MigrateDatabase()
            {
                using var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.SetConnectionString(connectionString);
                if (dbContext.Database.GetMigrations().Any())
                {
                    dbContext.Database.Migrate();
                }
            }

            #endregion Local methods
        }

        public static void RegisterService(this IServiceCollection services)
        {
            services.AddSingleton<IDatabaseProviderService, DatabaseProviderService>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}