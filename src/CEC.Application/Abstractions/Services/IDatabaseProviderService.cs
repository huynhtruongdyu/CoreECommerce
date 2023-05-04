using CEC.Application.Abstractions.Common;

namespace CEC.Application.Abstractions.Services
{
    public interface IDatabaseProviderService : ISingletonService
    {
        string GetProvider();

        string GetConnectionString();
    }
}