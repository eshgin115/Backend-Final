namespace Pronia.Areas.Client.ViewModels.Shop
{
    public class ListViewModel
    {

        public List<Admin.ViewModels.Subcategory.ListViewModel>? Subcategories { get; set; }
        public List<Admin.ViewModels.Color.ListViewModel>? Colors { get; set; }
        public List<Admin.ViewModels.Tag.ListViewModel>? Tags { get; set; }

        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string SearchQuery { get; set; }
        public int? ColorId { get; set; }
        public int? SubcategoryId { get; set; }
        public int? TagId { get; set; }
        public ListViewModel()
        {

        }

        public ListViewModel(List<Admin.ViewModels.Subcategory.ListViewModel>? subcategories, List<Admin.ViewModels.Color.ListViewModel>? colors, List<Admin.ViewModels.Tag.ListViewModel>? tags)
        {
            Subcategories = subcategories;
            Colors = colors;
            Tags = tags;
        }
    }
}
