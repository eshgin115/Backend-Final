using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class Subcategory : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public List<Plant>? Plants { get; set; } 
        public int Categoryİd { get; set; }
        public Category? Category { get; set; } 
        
    }
}
