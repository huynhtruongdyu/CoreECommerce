using CEC.Application.Contexts;
using CEC.Application.Services;
using CEC.Application.UnitOfWork;
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
                    case DatabaseProviderConstant.InMemory:
                        services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("InMemoryDb"));
                        break;

                    case DatabaseProviderConstant.MSSQL:
                        services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));
                        break;
                    //TODO: Implement other Database provider
                    default:
                        services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlite(connectionString));
                        break;
                }
            }

            void MigrateDatabase()
            {
                if (provider != DatabaseProviderConstant.InMemory)
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    //dbContext.Database.EnsureCreated();
                    dbContext.Database.SetConnectionString(connectionString);
                    if (dbContext.Database.GetMigrations().Any())
                    {
                        dbContext.Database.Migrate();
                    }
                }
            }

            #endregion Local methods
        }

        public static void RegisterService(this IServiceCollection services)
        {
            services.AddSingleton<IDatabaseProviderService, DatabaseProviderService>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IUserActivityLogService, UserActivityLogService>();
        }
    }
}