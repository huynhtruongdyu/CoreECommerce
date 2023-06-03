using CEC.Application.Contexts;
using CEC.Application.Services;
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
        private readonly IServiceManagement _serviceManagement;

        public ApplicationDbContext(DbContextOptions options, IServiceManagement serviceManagement)
            : base(options)
        {
            _serviceManagement = serviceManagement;
        }

        #region override methods

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
                var dbProvider = _serviceManagement.DatabaseProviderService.GetProvider();
                var connectionString = _serviceManagement.DatabaseProviderService.GetConnectionString();
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

        #endregion override methods

        #region DbSet

        public DbSet<Product> Products { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        #endregion DbSet

        public async Task ClearData()
        {
            this.Products.Clear();
            await this.SaveChangesAsync();
        }
    }
}