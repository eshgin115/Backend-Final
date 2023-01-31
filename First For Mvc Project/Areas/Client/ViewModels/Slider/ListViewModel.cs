namespace Pronia.Areas.Client.ViewModels.Slider
{
    public class ListViewModel
    {
        public string? Offer { get; set; }
        public string? Tittle { get; set; }
        public string? Content { get; set; }
        public string ImageURL { get; set; }
        public string? ButtonName { get; set; }
        public string? ButtonURL { get; set; }

        public ListViewModel(string? offer, string? tittle, string? content, string imageURL, string? buttonName, string? buttonURL)
        {
            Offer = offer;
            Tittle = tittle;
            Content = content;
            ImageURL = imageURL;
            ButtonName = buttonName;
            ButtonURL = buttonURL;
        }
        public ListViewModel()
        {

        }
    }
}
