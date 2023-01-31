using First_For_Mvc_Project.Database.Models.Common;

namespace First_For_Mvc_Project.Database.Models
{
    public class AboutComponent : BaseEntity<int>
    {
        public string Content { get; set; } = null!;

     
    }
}
