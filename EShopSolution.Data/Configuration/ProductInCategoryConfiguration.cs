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
    public class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            

            builder.ToTable("ProductInCategories");

            builder.HasKey(t => new { t.CategoryId, t.ProductId });

            builder.HasOne(c => c.Category)
                .WithMany(p => p.ProductInCategories)
                .HasForeignKey(c => c.CategoryId);

            builder.HasOne(p => p.Product)
                .WithMany(c => c.ProductInCategories)
                .HasForeignKey(p => p.ProductId);

        }

    }
}
