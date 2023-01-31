using Pronia.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pronia.Database.Configurations
{
    public class PlantImageConfigurations : IEntityTypeConfiguration<PlantImage>
    {
        public void Configure(EntityTypeBuilder<PlantImage> builder)
        {
            builder
                .ToTable("PlantImages");

            builder
                .HasOne(pi => pi.Plant)
                .WithMany(p => p.PlantImages)
                .HasForeignKey(pi => pi.PlantId);
        }
    }
}
