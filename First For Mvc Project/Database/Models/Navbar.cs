using First_For_Mvc_Project.Database.Models.Common;

namespace First_For_Mvc_Project.Database.Models
{
    public class Navbar : BaseEntity<int>, IAuditable
    {
        public string Name { get; set; } = null!;
        public string ToURL { get; set; } = null!;
        public int Order { get; set; }
        public bool IsViewOnHeader { get; set; }
        public bool IsViewOnFooter { get; set; }

        public List<Subnavbar>? Subnavbars { get; set; } 

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
