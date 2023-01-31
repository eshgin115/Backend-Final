using First_For_Mvc_Project.Database.Models.Common;

namespace First_For_Mvc_Project.Database.Models
{
    public class Category : BaseEntity<int>, IAuditable
    {
        public string Name { get; set; } = null!;

        public List<Subcategory>? Subcategories { get; set; } 

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
