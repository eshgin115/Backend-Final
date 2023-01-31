namespace Pronia.Areas.Admin.ViewModels.Plant
{
    public class ListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public decimal Price { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ImageURL { get; set; }
        public List<ViewModels.Tag.ListViewModel>? Tags { get; set; }
        public List<ViewModels.Size.ListViewModel>? Sizes { get; set; }
        public List<ViewModels.Color.ListViewModel>? Colors { get; set; }

        public string Category { get; set; }
        public string Subcategory { get; set; }=null!;

        public ListViewModel(int id, string title, string content, decimal price, DateTime createdAt, DateTime updatedAt, string imageURL, List<Tag.ListViewModel>? tags, List<Size.ListViewModel>? sizes, List<Color.ListViewModel>? colors, string category, string subcategory)
        {
            Id = id;
            Title = title;
            Content = content;
            Price = price;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            ImageURL = imageURL;
            Tags = tags;
            Sizes = sizes;
            Colors = colors;
            Category = category;
            Subcategory = subcategory;
        }
        public ListViewModel()
        {

        }
    }
}
