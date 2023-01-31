using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class Feedback : BaseEntity<int>, IAuditable
    {
        public string Name { get; set; } = null!;
        public string ImageName { get; set; } = null!;
        public string ImageNameInFileSystem { get; set; } = null!;
        public int? RoleId { get; set; }
        public Role? Role { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
