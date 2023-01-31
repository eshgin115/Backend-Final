namespace Pronia.Areas.Admin.ViewModels.Slider
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public IFormFile? Image { get; set; }
        public string ImageURL { get; set; }
        public string ButtonName { get; set; }
        public string ButtonURL { get; set; }
        public int Order { get; set; }
        public string? Offer { get; set; }
        public string? Tittle { get; set; }
    }
}
