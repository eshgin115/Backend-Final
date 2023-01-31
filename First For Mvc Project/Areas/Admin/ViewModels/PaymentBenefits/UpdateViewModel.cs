namespace Pronia.Areas.Admin.ViewModels.PaymentBenefits
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        public IFormFile? Image { get; set; }
        public string ImageURL { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public string Content { get; set; }
    }
}
