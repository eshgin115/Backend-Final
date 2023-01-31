using System.ComponentModel.DataAnnotations;

namespace First_For_Mvc_Project.Areas.Client.ViewModels.Account
{
    public class ContactViewModel
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Message { get; set; } = null!;
    }
}
