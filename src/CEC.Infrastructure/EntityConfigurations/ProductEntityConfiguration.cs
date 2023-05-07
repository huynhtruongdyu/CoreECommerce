using CEC.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEC.Infrastructure.EntityConfigurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public const string TableName = "Products";

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => x.IsDeleted == false);
            builder.Property(x => x.Name).IsRequired();
        }
    }
}