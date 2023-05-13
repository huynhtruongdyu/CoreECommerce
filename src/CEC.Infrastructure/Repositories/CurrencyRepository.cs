using CEC.Application.Repositories;
using CEC.Domain.Entities;
using CEC.Infrastructure.Contexts;

namespace CEC.Infrastructure.Repositories
{
    public class CurrencyRepository : GenericRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}