using Pronia.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pronia.Database.Configurations
{
    public class SubcategoryConfigurations : IEntityTypeConfiguration<Subcategory>
    {
        public void Configure(EntityTypeBuilder<Subcategory> builder)
        {
            builder
                .ToTable("Subcategories");
            builder
              .HasOne(sc => sc.Category)
              .WithMany(p => p.Subcategories)
              .HasForeignKey(sc => sc.Categoryİd);


          
        }
    }
}
