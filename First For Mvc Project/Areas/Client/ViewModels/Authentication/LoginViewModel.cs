using System.ComponentModel.DataAnnotations;

namespace First_For_Mvc_Project.Areas.Client.ViewModels.Authentication
{
    public class LoginViewModel
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
