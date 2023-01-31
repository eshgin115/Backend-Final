namespace First_For_Mvc_Project.Areas.Admin.ViewModels.Navbar
{
    public class ListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public ListViewModel(int id, string name, int order)
        {
            Id = id;
            Name = name;
            Order = order;
        }

        public ListViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
