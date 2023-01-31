using First_For_Mvc_Project.Database.Models.Common;

namespace First_For_Mvc_Project.Database.Models
{
    public class Plant : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public decimal Price { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<PlantTag>? PlantTags { get; set; }
        public List<PlantSize>? PlantSizes { get; set; }
        public List<PlantColor>? PlantColors { get; set; }
        public List<PlantImage>? PlantImages { get; set; }
        public List<BasketProduct>? BasketProducts { get; set; }
        public List<OrderProduct>? OrderProducts { get; set; }

        public int SubcategoryId { get; set; }
        public Subcategory? Subcategory { get; set; }
    }
}
