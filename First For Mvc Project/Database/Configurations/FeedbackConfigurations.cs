using First_For_Mvc_Project.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace First_For_Mvc_Project.Database.Configurations
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
