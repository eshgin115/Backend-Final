using First_For_Mvc_Project.Database.Models.Common;

namespace First_For_Mvc_Project.Database.Models
{
    public class OrderProduct : BaseEntity<int>, IAuditable
    {
        public string? Orderİd { get; set; }
        public Order? Order { get; set; }

        public int? PlantId { get; set; }
        public Plant? Plant { get; set; }

        public int? Quantity { get; set; }
        public int? SizeId { get; set; }
        public Size? Size { get; set; }

        public int? ColorId { get; set; }
        public Color? Color { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
