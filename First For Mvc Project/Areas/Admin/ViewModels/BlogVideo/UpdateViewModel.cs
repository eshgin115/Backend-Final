namespace Pronia.Areas.Admin.ViewModels.BlogVideo
{
    public class UpdateViewModel
    {
        public string? VideoUrL { get; set; }
        public int? Order { get; set; }
        public IFormFile? Video { get; set; }
    }
}
