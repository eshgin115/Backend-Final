using System.ComponentModel.DataAnnotations;

namespace First_For_Mvc_Project.Areas.Admin.ViewModels.Brand
{
    public class AddViewModel
    {
        [Required]
        public IFormFile Photo { get; set; }
    }
}
