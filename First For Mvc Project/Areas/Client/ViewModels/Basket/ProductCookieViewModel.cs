namespace Pronia.Areas.Client.ViewModels.Basket
{
    public class ProductCookieViewModel
    {


        public ProductCookieViewModel(int id, string? title, string? imageUrl, int? quantity, decimal? price, decimal? total, int? sizeId, int? colorId, List<Admin.ViewModels.Size.ListViewModel>? sizes, List<Admin.ViewModels.Color.ListViewModel>? colors)
        {
            Id = id;
            Title = title;
            ImageUrl = imageUrl;
            Quantity = quantity;
            Price = price;
            Total = total;
            SizeId = sizeId;
            ColorId = colorId;
            Sizes = sizes;
            Colors = colors;
        }
        public ProductCookieViewModel()
        {

        }

     

  

        public ProductCookieViewModel(int id, string? title, string? imageUrl, int? quantity, decimal? price, decimal? total)
        {
            Id = id;
            Title = title;
            ImageUrl = imageUrl;
            Quantity = quantity;
            Price = price;
            Total = total;
        }

        public ProductCookieViewModel(int id, string? title, string? imageUrl, int? quantity, decimal? price, decimal? total, List<Admin.ViewModels.Size.ListViewModel>? sizes, List<Admin.ViewModels.Color.ListViewModel>? colors)
        {
            Id = id;
            Title = title;
            ImageUrl = imageUrl;
            Quantity = quantity;
            Price = price;
            Total = total;
            Sizes = sizes;
            Colors = colors;
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Total { get; set; }
        public List<Admin.ViewModels.Size.ListViewModel>? Sizes { get; set; }
        public List<Admin.ViewModels.Color.ListViewModel>? Colors { get; set; }



        public int? SizeId { get; set; }
        public int? ColorId { get; set; }
    }
}
