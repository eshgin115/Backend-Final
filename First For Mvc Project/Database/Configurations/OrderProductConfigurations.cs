using Pronia.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pronia.Database.Configurations
{
    public class OrderProductConfigurations : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder
                .ToTable("OrderProducts");

            builder
              .HasOne(op => op.Order)
              .WithMany(order => order.OrderProducts)
              .HasForeignKey(op => op.Orderİd);

            builder
              .HasOne(op => op.Plant)
              .WithMany(plant => plant.OrderProducts)
              .HasForeignKey(op => op.PlantId);

        }
    }
}
