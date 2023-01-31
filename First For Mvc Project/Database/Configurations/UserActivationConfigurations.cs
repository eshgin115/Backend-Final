using Pronia.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pronia.Database.Configurations
{
    public class UserActivationConfigurations : IEntityTypeConfiguration<UserActivation>
    {
        public void Configure(EntityTypeBuilder<UserActivation> builder)
        {
            builder
               .ToTable("UserActivations");
            builder
            .HasOne(ua => ua.User)
            .WithOne(u => u.UserActivation)
            .HasForeignKey<User>(u => u.UserActivationId);
        }
    }
}
