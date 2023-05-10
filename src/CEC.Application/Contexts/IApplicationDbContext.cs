using CEC.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace CEC.Application.Contexts
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; set; }

        Task ClearData();
    }
}