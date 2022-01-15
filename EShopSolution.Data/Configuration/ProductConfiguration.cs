using EShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopSolution.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Price).IsRequired();

            builder.Property(x => x.OriginalPrice).IsRequired();

            builder.Property(x => x.Stock)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(x => x.ViewCount)
                .HasDefaultValue(0)
                .IsRequired();
        }
    }
}
