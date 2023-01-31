namespace Pronia.Areas.Admin.ViewModels.Slider
{
    public class ListViewModel
    {
        public int Id { get; set; }
        public string? Offer { get; set; }
        public string? Tittle { get; set; }
        public string? Content { get; set; }
        public string ImageURL { get; set; }
        public string? ButtonName { get; set; }
        public string? ButtonURL { get; set; }
        public int Order { get; set; }

        public ListViewModel(int id, string? offer, string? tittle, string? content, string imageURL, string? buttonName, string? buttonURL, int order)
        {
            Id = id;
            Offer = offer;
            Tittle = tittle;
            Content = content;
            ImageURL = imageURL;
            ButtonName = buttonName;
            ButtonURL = buttonURL;
            Order = order;
        }
    }
}
