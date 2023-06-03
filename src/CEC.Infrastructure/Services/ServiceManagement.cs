using CEC.Application.Services;
using CEC.Shared.Options;

using Microsoft.Extensions.Options;

namespace CEC.Infrastructure.Services
{
    public class ServiceManagement : IServiceManagement
    {
        private readonly Lazy<IDatabaseProviderService> _databaseProviderService;
        private readonly Lazy<IUserActivityLogService> _userActivityLogService;

        public ServiceManagement(IOptions<GlobalAppsettings> globalAppsettingOpt)
        {
            _databaseProviderService = new Lazy<IDatabaseProviderService>(() => new DatabaseProviderService(globalAppsettingOpt));
            _userActivityLogService = new Lazy<IUserActivityLogService>(() => new UserActivityLogService());
        }

        public IDatabaseProviderService DatabaseProviderService => _databaseProviderService.Value;

        public IUserActivityLogService UserActivityLogService => _userActivityLogService.Value;
    }
}