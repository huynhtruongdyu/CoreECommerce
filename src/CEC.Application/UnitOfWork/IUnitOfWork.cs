using CEC.Application.Repositories;

namespace CEC.Application.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        IProductRepository ProductRepository { get; }
    }
}