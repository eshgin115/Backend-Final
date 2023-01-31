using First_For_Mvc_Project.Database.Models.Common;

namespace First_For_Mvc_Project.Database.Models
{
    public class Basket : BaseEntity<int>, IAuditable
    {
        public int UserId { get; set; }
        public User? User { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<BasketProduct>? BasketProducts { get; set; }
    }
}
