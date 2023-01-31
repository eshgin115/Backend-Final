namespace First_For_Mvc_Project.Areas.Admin.ViewModels.Subnavbar
{
    public class ListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NavbarName { get; set; }

        public ListViewModel(int id, string name, string navbarName)
        {
            Id = id;
            Name = name;
            NavbarName = navbarName;

        }
    }
}
