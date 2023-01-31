using First_For_Mvc_Project.Database.Models.Common;

namespace First_For_Mvc_Project.Database.Models
{
    public class Subcategory : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public List<Plant>? Plants { get; set; } 
        public int Categoryİd { get; set; }
        public Category? Category { get; set; } 
        
    }
}
