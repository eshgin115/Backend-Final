using First_For_Mvc_Project.Database.Models.Common;

namespace First_For_Mvc_Project.Database.Models
{
    public class Slider : BaseEntity<int>, IAuditable
    {
        public string Offer { get; set; } = null!;
        public string Tittle { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string ImageName { get; set; } = null!;
        public string ImageNameInFileSystem { get; set; } = null!;
        public string ButtonName { get; set; } = null!;
        public string ButtonURL { get; set; } = null!;
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
