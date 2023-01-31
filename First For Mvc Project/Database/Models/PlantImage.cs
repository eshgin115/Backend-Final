using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class PlantImage : BaseEntity<int>, IAuditable
    {
        public string ImageName { get; set; } = null!;
        public string ImageNameInFileSystem { get; set; } = null!;
        public int Order { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int PlantId { get; set; }
        public Plant? Plant { get; set; }
    }
}
