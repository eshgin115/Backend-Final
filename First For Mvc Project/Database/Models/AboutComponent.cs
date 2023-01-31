using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class AboutComponent : BaseEntity<int>
    {
        public string Content { get; set; } = null!;

     
    }
}
