using CEC.Application.Abstractions.Contexts;
using CEC.Application.Abstractions.Services;
using CEC.Domain.Common;
using CEC.Domain.Entities;
using CEC.Infrastructure.Extensions;
using CEC.Shared.Constants;
using CEC.Shared.Extensions;

using Microsoft.EntityFrameworkCore;

namespace CEC.Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDatabaseProviderService _databaseProviderService;

        public ApplicationDbContext(DbContextOptions options, IDatabaseProviderService databaseProviderService)
            : base(options)
        {
            _databaseProviderService = databaseProviderService;
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                            .Entries()
                            .Where(e => e.Entity is AuditEntity && (
                                    e.State == EntityState.Added
                                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((AuditEntity)entityEntry.Entity).UpdatedAt = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((AuditEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                            .Entries()
                            .Where(e => e.Entity is AuditEntity && (
                                    e.State == EntityState.Added
                                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((AuditEntity)entityEntry.Entity).UpdatedAt = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((AuditEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(InfrastructureAssemblyReference.Assembly);
            modelBuilder.ApplySoftDeleteQueryFilter();
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var dbProvider = _databaseProviderService.GetProvider();
                var connectionString = _databaseProviderService.GetConnectionString();
                var assemblyName = InfrastructureAssemblyReference.Assembly.GetName().Name;

                switch (dbProvider)
                {
                    case DatabaseProviderConstant.InMemory:
                        optionsBuilder.UseInMemoryDatabase("InMemoryDb");
                        break;

                    case DatabaseProviderConstant.MSSQL:
                        optionsBuilder.UseSqlServer(connectionString, opt => opt.MigrationsAssembly(assemblyName));
                        break;

                    //Sqlite
                    default:
                        optionsBuilder.UseSqlite(connectionString, opt => opt.MigrationsAssembly(assemblyName));
                        break;
                }
            }
        }

        public async Task ClearData()
        {
            this.Products.Clear();
            await this.SaveChangesAsync();
        }

        public DbSet<Product> Products { get; set; }
    }
}