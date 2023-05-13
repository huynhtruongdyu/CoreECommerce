using CEC.Application.Services;
using CEC.Shared.Models;

namespace CEC.Infrastructure.Services
{
    public class UserActivityLogService : IUserActivityLogService
    {
        private static List<UserAcionLog> userAcionLogList;

        static UserActivityLogService()
        {
            userAcionLogList = new List<UserAcionLog>();
        }

        public void Log(UserAcionLog userAcionLog)
        {
            userAcionLogList.Add(userAcionLog);
        }

        public IReadOnlyCollection<string> GetLog() => userAcionLogList.OrderByDescending(x => x.CreatedAt).Select(x => x.ToString()).ToList();
    }
}