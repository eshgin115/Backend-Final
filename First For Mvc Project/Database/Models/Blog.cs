using First_For_Mvc_Project.Database.Models.Common;

namespace First_For_Mvc_Project.Database.Models
{
    public class Blog : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int? AdminId { get; set; }
        public User? Admin { get; set; }
        public List<BlogTag>? BlogTags { get; set; }
        public List<BlogImage>? BlogImages { get; set; }
        public List<BlogVideo>? BlogVideos { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
