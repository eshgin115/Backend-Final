namespace First_For_Mvc_Project.Areas.Client.ViewModels.Blog
{
    public class ListViewModel
    {
        public List<Pronia.Areas.Admin.ViewModels.Tag.ListViewModel>? Tags { get; set; }

        public string SearchQuery { get; set; }
        public int? TagId { get; set; }
        public ListViewModel()
        {

        }

        public ListViewModel(List<Pronia.Areas.Admin.ViewModels.Tag.ListViewModel>? tags)
        {
            Tags = tags;
        }
    }
}
