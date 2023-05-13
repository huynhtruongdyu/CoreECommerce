using CEC.Application.Common;
using CEC.Shared.Models;

namespace CEC.Application.Services
{
    public interface IUserActivityLogService : ISingletonService
    {
        void Log(UserAcionLog userAcionLog);

        IReadOnlyCollection<string> GetLog();
    }
}