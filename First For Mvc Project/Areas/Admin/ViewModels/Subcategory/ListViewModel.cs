namespace Pronia.Areas.Admin.ViewModels.Subcategory
{
    public class ListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }

        public ListViewModel(int id, string name)
        {
            Id = id;
            Name = name;

        }
        public ListViewModel(int id, string name, string categoryName)
        {
            Id = id;
            Name = name;
            CategoryName = categoryName;

        }
    }
}
