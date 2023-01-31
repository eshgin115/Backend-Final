using First_For_Mvc_Project.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace First_For_Mvc_Project.Database.Configurations
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
