using CEC.Application.Services;
using CEC.Domain.Entities;
using CEC.Shared.Models;

namespace CEC.Infrastructure.Services
{
    public class UserActivityLogService : IUserActivityLogService
    {
        private static List<UserAcionLog> userAcionLogList;

        static UserActivityLogService()
        {
            userAcionLogList = new List<UserAcionLog>()
            {
                new UserAcionLog("Admin", EnumUserAcion.Add, new Product("Sample Product 1")),
                new UserAcionLog("Admin", EnumUserAcion.Update, new Product("Sample Product 2")),
                new UserAcionLog("Admin", EnumUserAcion.Add, new Product("Sample Product 3")),
                new UserAcionLog("Admin", EnumUserAcion.Add, new Product("Sample Product 4")),
                new UserAcionLog("Admin", EnumUserAcion.Update, new Product("Sample Product 5")),
                new UserAcionLog("Admin", EnumUserAcion.Add, new Product("Sample Product 6")),
                new UserAcionLog("Admin", EnumUserAcion.Remove, new Product("Sample Product 7")),
                new UserAcionLog("Admin", EnumUserAcion.Add, new Product("Sample Product 8")),
                new UserAcionLog("Admin", EnumUserAcion.Update, new Product("Sample Product 9")),
                new UserAcionLog("Admin", EnumUserAcion.Add, new Product("Sample Product 10")),
                new UserAcionLog("Admin", EnumUserAcion.Remove, new Product("Sample Product 11")),
            };
        }

        public void Log(UserAcionLog userAcionLog)
        {
            userAcionLogList.Add(userAcionLog);
        }

        public IReadOnlyCollection<string> GetLog() => userAcionLogList.OrderByDescending(x => x.CreatedAt).Select(x => x.ToString()).ToList();
    }
}