using First_For_Mvc_Project.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace First_For_Mvc_Project.Database.Configurations
{
    public class PlantTagConfigurations : IEntityTypeConfiguration<PlantTag>
    {
        public void Configure(EntityTypeBuilder<PlantTag> builder)
        {
            builder
                .ToTable("PlantTags");


            builder
                .HasKey(pt => new { pt.PlantId, pt.TagId });

            builder
               .HasOne(pt => pt.Plant)
               .WithMany(p => p.PlantTags)
               .HasForeignKey(pt => pt.PlantId);

            builder
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PlantTags)
                .HasForeignKey(pt => pt.TagId);
        }
    }
}
