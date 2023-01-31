using Pronia.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pronia.Database.Configurations
{
    public class FeedbackConfigurations : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder
                .ToTable("Feedbacks");
            builder
               .HasOne(f => f.Role)
               .WithMany(r => r.Feedbacks)
               .HasForeignKey(f => f.RoleId);
        }
    }
}
