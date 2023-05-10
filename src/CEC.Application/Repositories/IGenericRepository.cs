using CEC.Domain.Common;

using System.Linq.Expressions;

namespace CEC.Application.Repositories
{
    public interface IGenericRepository<T> : IDisposable where T : IRootEntity
    {
        IEnumerable<T> Get(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");

        IEnumerable<T> GetAll();

        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

        T GetById(object id);

        Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default);

        void Insert(T entity);

        Task InsertAsync(T entity, CancellationToken cancellationToken = default);

        void Update(T entity);

        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

        void Delete(object id);

        Task DeleteAsync(object id, CancellationToken cancellationToken = default);

        void SaveChanges();

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}