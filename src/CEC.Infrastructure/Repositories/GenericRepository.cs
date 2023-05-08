using CEC.Application.Abstractions.Repositories;
using CEC.Domain.Common;
using CEC.Infrastructure.Contexts;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace CEC.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class, IRootEntity
    {
        protected readonly ApplicationDbContext dbContext;
        protected readonly DbSet<T> dbSet;
        private bool disposed = false;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }

        public void Delete(object id)
        {
            var entity = dbSet.Find(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                dbContext.SaveChanges();
            }
        }

        public async Task DeleteAsync(object id, CancellationToken cancellationToken = default)
        {
            var entity = await dbSet.FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            return await dbSet.FindAsync(id, cancellationToken);
        }

        public void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public async Task InsertAsync(T entity, CancellationToken cancellationToken = default)
        {
            await dbSet.AddAsync(entity, cancellationToken);
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Update(T entity)
        {
            //First attach the object to the table
            dbSet.Attach(entity);
            //Then set the state of the Entity as Modified
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            dbSet.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}