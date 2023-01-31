using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class Color : BaseEntity<int>, IAuditable
    {
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<PlantColor>? PlantColors { get; set; }
        public List<BasketProduct>? BasketProducts { get; set; }
        public List<OrderProduct>? OrderProducts { get; set; }

    }
}
