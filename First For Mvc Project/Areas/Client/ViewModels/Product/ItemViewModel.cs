namespace Pronia.Areas.Client.ViewModels.Product
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public decimal Price { get; set; }
        public string ImageURL { get; set; }
        public int? Quantity { get; set; }

        public List<Admin.ViewModels.Size.ListViewModel>? Sizes { get; set; }
        public List<Admin.ViewModels.Color.ListViewModel>? Colors { get; set; }


        public int? ColorId { get; set; }
        public int? SizeId { get; set; }


        public ItemViewModel(int? colorId, int? sizeId, int? quantity)
        {
            ColorId = colorId;
            SizeId = sizeId;
            Quantity = quantity;
        }

        public ItemViewModel(int id, string title, string content, decimal price,string imageURL, List<Admin.ViewModels.Size.ListViewModel>? sizes, List<Admin.ViewModels.Color.ListViewModel>? colors)
        {
            Id = id;
            Title = title;
            Content = content;
            Price = price;
            ImageURL = imageURL;
            Sizes = sizes;
            Colors = colors;
        }

        public ItemViewModel()
        {

        }

     

    }
}
