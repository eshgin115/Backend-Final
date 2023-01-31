using System.ComponentModel.DataAnnotations;

namespace Pronia.Areas.Client.ViewModels.Account
{
    public class UpdateUserDataViewModel
    {
        [Required]
        public string? CurrentPassword { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? LastName { get; set; }
    }
}
