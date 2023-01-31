using Pronia.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pronia.Database.Configurations
{
    public class AboutComponentConfigurations : IEntityTypeConfiguration<AboutComponent>
    {
        private int _idCounter = 1;
        public void Configure(EntityTypeBuilder<AboutComponent> builder)
        {
            builder
               .ToTable("AboutComponents");


            builder
                .HasData(
                    new AboutComponent
                    {
                        Id = _idCounter++,
                        Content = "Define Content"
                    }

                );
        }
    }
}
