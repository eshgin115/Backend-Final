using First_For_Mvc_Project.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace First_For_Mvc_Project.Database.Configurations
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
