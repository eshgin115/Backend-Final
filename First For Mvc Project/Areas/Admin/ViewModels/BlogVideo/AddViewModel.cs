namespace Pronia.Areas.Admin.ViewModels.BlogVideo
{
    public class AddViewModel
    {
        public int? Order { get; set; }
        public IFormFile? Video { get; set; }
        public string? VideoURLFromBrauser { get; set; }
    }
}
