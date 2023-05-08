using CEC.Application.Abstractions.Repositories;
using CEC.Domain.Entities;
using CEC.Infrastructure.Contexts;

namespace CEC.Infrastructure.Repositories
{
    internal class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}