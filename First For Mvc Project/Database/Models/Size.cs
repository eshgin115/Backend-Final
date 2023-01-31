using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class Size :BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<PlantSize>? PlantSizes { get; set; }
        public List<BasketProduct>? BasketProducts { get; set; }
        public List<OrderProduct>? OrderProducts { get; set; }
    }
}
