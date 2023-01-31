namespace First_For_Mvc_Project.Areas.Admin.ViewModels.Plant
{
    public class AddViewModel
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public decimal Price { get; set; } 
        public List<ViewModels.Tag.ListViewModel>? Tags { get; set; }
        public List<int> TagIds { get; set; }
        public List<ViewModels.Size.ListViewModel>? Sizes { get; set; }
        public List<int> SizeIds { get; set; }
        public List<ViewModels.Color.ListViewModel>? Colors { get; set; }
        public List<int> ColorIds { get; set; }
        public List<ViewModels.Subcategory.ListViewModel>? Subcategories { get; set; }
        public int SubcategoryId { get; set; }
     
    }
}
