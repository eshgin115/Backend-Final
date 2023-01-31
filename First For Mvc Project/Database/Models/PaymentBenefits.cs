using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class PaymentBenefits : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; } = null!;
        public int Order { get; set; }
        public string Content { get; set; } = null!;
        public string ImageName { get; set; } = null!;
        public string ImageNameInFileSystem { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
