using First_For_Mvc_Project.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace First_For_Mvc_Project.Database.Configurations
{
    public class PaymentBenefitsConfigurations : IEntityTypeConfiguration<PaymentBenefits>
    {
        public void Configure(EntityTypeBuilder<PaymentBenefits> builder)
        {
            builder
              .ToTable("PaymentBenefits");
        }
    }
}
