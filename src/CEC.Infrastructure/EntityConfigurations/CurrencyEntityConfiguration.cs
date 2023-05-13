using CEC.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEC.Infrastructure.EntityConfigurations
{
    public class CurrencyEntityConfiguration : IEntityTypeConfiguration<Currency>
    {
        public const string TableName = "Currencies";

        //src: https://gist.github.com/voskobovich/537b2000108e4781f70b
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.Symbol).IsRequired();
            builder.HasData(new[]
            {
                new Currency("Dong","VND","đ"),
                new Currency("Dollars","USD","$"),
            });
        }
    }
}