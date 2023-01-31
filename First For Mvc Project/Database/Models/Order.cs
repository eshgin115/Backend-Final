using First_For_Mvc_Project.Contracts.Order;
using First_For_Mvc_Project.Database.Models.Common;

namespace First_For_Mvc_Project.Database.Models
{
    public class Order : BaseEntity<string>, IAuditable
    {
        public int UserId { get; set; }
        public User? User { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<OrderProduct>? OrderProducts { get; set; }
        public Status Status { get; set; }

        public int SumTotalPrice { get; set; }
    }
}
