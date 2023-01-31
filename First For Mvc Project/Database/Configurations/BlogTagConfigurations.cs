using First_For_Mvc_Project.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace First_For_Mvc_Project.Database.Configurations
{
    public class BlogTagConfigurations : IEntityTypeConfiguration<BlogTag>
    {
        public void Configure(EntityTypeBuilder<BlogTag> builder)
        {
            builder
                .ToTable("BlogTags");


            builder
                .HasKey(bt => new { bt.BlogId, bt.TagId });

            builder
               .HasOne(bt => bt.Blog)
               .WithMany(b => b.BlogTags)
               .HasForeignKey(bt => bt.BlogId);

            builder
                .HasOne(bt => bt.Tag)
                .WithMany(t => t.BlogTags)
                .HasForeignKey(bt => bt.TagId);
        }
    }
}
