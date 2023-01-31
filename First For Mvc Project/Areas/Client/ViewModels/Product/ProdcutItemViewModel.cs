namespace First_For_Mvc_Project.Areas.Client.ViewModels.Product
{
    public class ProdcutItemViewModel
    {
        public ProdcutItemViewModel(int id, string title, string content, decimal price,
            List<ImageItemViewModel> imageURLs, string categoryName, string subcategoryName, List<TagItemViewModel> tags, int subcategoryId)
        {
            Id = id;
            Title = title;
            Content = content;
            Price = price;
            ImageURLs = imageURLs;
            CategoryName = categoryName;
            SubcategoryName = subcategoryName;
            Tags = tags;
            SubcategoryId = subcategoryId;
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public decimal Price { get; set; }
        public List<ImageItemViewModel> ImageURLs { get; set; }
        public string CategoryName { get; set; }
        public string SubcategoryName { get; set; }
        public List<TagItemViewModel> Tags { get; set; }
        public int SubcategoryId { get; set; }


        public class ImageItemViewModel
        {
            public ImageItemViewModel(string imageName, string imageUrl)
            {
                ImageName = imageName;
                ImageUrl = imageUrl;
            }

            public string ImageName { get; set; }
            public string ImageUrl { get; set; }
        }
        public class TagItemViewModel
        {
            public TagItemViewModel(string tagName, int tagId)
            {
                TagName = tagName;
                TagId = tagId;
            }

            public string TagName { get; set; }
            public int TagId { get; set; }
        }
    }
}
