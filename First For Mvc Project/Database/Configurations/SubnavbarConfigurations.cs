using Pronia.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pronia.Database.Configurations
{
    public class SubnavbarConfiguration : IEntityTypeConfiguration<Subnavbar>
    {
        public void Configure(EntityTypeBuilder<Subnavbar> builder)
        {
            builder
              .ToTable("Subnavbars");
            builder
             .HasOne(s => s.Navbar)
             .WithMany(n => n.Subnavbars)
             .HasForeignKey(s => s.NavbarId);
        }
    }
}
