using CEC.Application.Abstractions.Contexts;
using CEC.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace CEC.Persistent.Contexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}