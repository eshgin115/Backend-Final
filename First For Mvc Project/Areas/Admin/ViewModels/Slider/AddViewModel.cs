namespace First_For_Mvc_Project.Areas.Admin.ViewModels.Slider
{
    public class AddViewModel
    {
        public string Content { get; set; }
        public IFormFile? Image { get; set; }
        public string ButtonName { get; set; }
        public string ButtonURL { get; set; }
        public int Order { get; set; }
        public string? Offer { get; set; }
        public string? Tittle { get; set; }
    }
}
