namespace Pronia.Areas.Admin.ViewModels.Blog
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public List<ViewModels.Tag.ListViewModel>? Tags { get; set; }
        public List<int> TagIds { get; set; }
   
    }
}
