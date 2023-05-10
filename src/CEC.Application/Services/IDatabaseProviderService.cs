using CEC.Application.Common;

namespace CEC.Application.Services
{
    public interface IDatabaseProviderService : ISingletonService
    {
        string GetProvider();

        string GetConnectionString();
    }
}