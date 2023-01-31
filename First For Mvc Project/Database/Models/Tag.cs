using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class Tag : BaseEntity<int>, IAuditable
    {
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<PlantTag>? PlantTags { get; set; }
        public List<BlogTag>? BlogTags { get; set; }
    }
}
