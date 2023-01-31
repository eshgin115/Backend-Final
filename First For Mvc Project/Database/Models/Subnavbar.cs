using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class Subnavbar : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string ToURL { get; set; } = null!;
        public int Order { get; set; }
        public int NavbarId { get; set; }
        public Navbar? Navbar { get; set; } 
    }
}
