using Pronia.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pronia.Database.Configurations
{
    public class PlantColorConfigurations : IEntityTypeConfiguration<PlantColor>
    {
        public void Configure(EntityTypeBuilder<PlantColor> builder)
        {
            builder
                .ToTable("PlantColors");

            
            builder
                .HasKey(pc => new { pc.PlantId, pc.ColorId });

            builder
               .HasOne(pc => pc.Plant)
               .WithMany(p => p.PlantColors)
               .HasForeignKey(pc => pc.PlantId);

            builder
                .HasOne(pc => pc.Color)
                .WithMany(c => c.PlantColors)
                .HasForeignKey(pc => pc.ColorId);
        }
    }
}
