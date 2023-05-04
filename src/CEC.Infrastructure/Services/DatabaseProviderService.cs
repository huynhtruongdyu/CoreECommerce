using CEC.Application.Abstractions.Services;
using CEC.Shared.Constants;
using CEC.Shared.Options;

using Microsoft.Extensions.Options;

namespace CEC.Infrastructure.Services
{
    public class DatabaseProviderService : IDatabaseProviderService
    {
        private readonly string defaultProvider = DatabaseProviderConstant.Sqlite;
        private readonly string defaultConnectionString = "Data Source = DemoECommerce.db";

        public DatabaseProviderService(IOptions<GlobalAppsettings> appsettingsOpt)
        {
            var databaseSettings = appsettingsOpt.Value.DatabaseSettings;
            if (databaseSettings != null)
            {
                var provider = databaseSettings.Providers.Where(x => x.Name == databaseSettings.DefaultProvider).FirstOrDefault();
                if (provider != null)
                {
                    defaultProvider = provider.Name;
                    defaultConnectionString = provider.ConnectionString;
                }
            }
        }

        public string GetConnectionString() => defaultConnectionString;

        public string GetProvider() => defaultProvider;
    }
}