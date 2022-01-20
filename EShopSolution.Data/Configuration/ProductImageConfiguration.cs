﻿using EShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopSolution.Data.Configuration
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn();
            builder.Property(x => x.Caption).HasMaxLength(200);
            builder.Property(x => x.ImagePath).HasMaxLength(200).IsRequired(true);

            builder.HasOne(x => x.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(c => c.ProductId);

        }
    }
}