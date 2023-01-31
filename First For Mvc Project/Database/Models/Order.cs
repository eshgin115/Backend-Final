using Pronia.Contracts.Order;
using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
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
