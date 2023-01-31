using First_For_Mvc_Project.Database.Models.Common;

namespace First_For_Mvc_Project.Database.Models
{
    public class UserActivation : BaseEntity<int>, IAuditable
    {
        public string? ActivationUrl { get; set; }
        public string? ActivationToken { get; set; }
        public DateTime ExpireDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
