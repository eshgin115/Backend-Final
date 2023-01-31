using Pronia.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pronia.Database.Configurations
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
