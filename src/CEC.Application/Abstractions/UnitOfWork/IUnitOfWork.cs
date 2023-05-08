using CEC.Application.Abstractions.Repositories;

namespace CEC.Application.Abstractions.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        IProductRepository ProductRepository { get; }
    }
}