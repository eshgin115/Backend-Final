namespace Pronia.Areas.Client.ViewModels.Product
{
    public class ListViewModel
    {
    
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string MainImageUrl { get; set; }
        public string HoverImageUrl { get; set; }
        public ListViewModel(int id, string name, decimal? price, string mainImageUrl, string hoverImageUrl)
        {
            Id = id;
            Name = name;
            Price = price;
            MainImageUrl = mainImageUrl;
            HoverImageUrl = hoverImageUrl;
        }
    }
}
