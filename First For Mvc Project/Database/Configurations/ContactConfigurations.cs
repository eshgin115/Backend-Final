using Pronia.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pronia.Database.Configurations
{
    public class ContactConfigurations : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder
                .ToTable("Contacts");
            builder
            .HasOne(c => c.User)
            .WithOne(u => u.Contact)
            .HasForeignKey<User>(u => u.ContactId);
        }

    }
}
