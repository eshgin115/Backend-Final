namespace First_For_Mvc_Project.Areas.Admin.ViewModels.Subcategory
{
   
    public class UpdateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Categoryİd { get; set; }
        public List<ViewModels.Category.ListViewModel>? Categories { get; set; }
    }
}
