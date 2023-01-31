namespace Pronia.Areas.Client.ViewModels.Shopping
{
    public class OrderViewModel
    {
    

        public int SumTotal { get; set; }
        public List<ItemViewModel> Models { get; set; }
        public class ItemViewModel
        {
            public ItemViewModel(int id, string? title, int? quantity, decimal? total)
            {
                Id = id;
                Title = title;
                Quantity = quantity;
                Total = total;
            }

            public ItemViewModel(int id, string? title, int? quantity, decimal? price, decimal? total, int? sizeId, int? colorId)
            {
                Id = id;
                Title = title;
                Quantity = quantity;
                Price = price;
                Total = total;
                SizeId = sizeId;
                ColorId = colorId;
            }

            public int Id { get; set; }
            public string? Title { get; set; }
            public int? Quantity { get; set; }
            public decimal? Price { get; set; }
            public decimal? Total { get; set; }
            public int? SizeId { get; set; }
            public int? ColorId { get; set; }
        }
    }
}
