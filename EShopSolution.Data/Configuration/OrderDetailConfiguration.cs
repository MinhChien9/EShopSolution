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
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");

            builder.HasKey(od => new { od.OrderId, od.ProductId });

            builder.HasOne(o => o.Order)
                .WithMany(od => od.OrderDetails)
                .HasForeignKey(o => o.OrderId);

            builder.HasOne(p => p.Product)
                .WithMany(od => od.OrderDetails)
                .HasForeignKey(o => o.ProductId);
        }

    }
}
