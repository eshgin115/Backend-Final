namespace Pronia.Areas.Admin.ViewModels.Subcategory
{
    public class AddViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public List<ViewModels.Category.ListViewModel>? Categories { get; set; }
    }
}
