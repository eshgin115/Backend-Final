namespace First_For_Mvc_Project.Areas.Admin.ViewModels.PaymentBenefits
{
    public class AddViewModel
    {
        public IFormFile? Image { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public string Content { get; set; }
    }
}
