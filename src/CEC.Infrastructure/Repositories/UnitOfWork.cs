using CEC.Application.Repositories;
using CEC.Application.UnitOfWork;
using CEC.Infrastructure.Contexts;

namespace CEC.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProductRepository _productRepository;
        private ICurrencyRepository _currencyRepository;

        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public IProductRepository ProductRepository { get => _productRepository ??= new ProductRepository(_context); }
        public ICurrencyRepository CurrencyRepository { get => _currencyRepository ??= new CurrencyRepository(_context); }

        #region Dispose

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Dispose
    }
}