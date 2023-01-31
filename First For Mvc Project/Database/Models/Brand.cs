using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class Brand : BaseEntity<int>, IAuditable
    {
        public string ImageName { get; set; } = null!;
        public string ImageNameInFileSystem { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
