using First_For_Mvc_Project.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace First_For_Mvc_Project.Database.Configurations
{
    public class PlantConfigurations : IEntityTypeConfiguration<Plant>
    {
        public void Configure(EntityTypeBuilder<Plant> builder)
        {
            builder
               .ToTable("Plants");
            builder.HasOne(p => p.Subcategory)
                   .WithMany(sc => sc.Plants)
                   .HasForeignKey(p => p.SubcategoryId);
        }
    }
}
