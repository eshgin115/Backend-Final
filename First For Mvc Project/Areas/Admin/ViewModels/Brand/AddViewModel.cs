using System.ComponentModel.DataAnnotations;

namespace Pronia.Areas.Admin.ViewModels.Brand
{
    public class AddViewModel
    {
        [Required]
        public IFormFile Photo { get; set; }
    }
}
