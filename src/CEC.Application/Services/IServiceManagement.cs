using CEC.Application.Common;

namespace CEC.Application.Services
{
    public interface IServiceManagement : IScopedService
    {
        IDatabaseProviderService DatabaseProviderService { get; }
        IUserActivityLogService UserActivityLogService { get; }
    }
}