namespace First_For_Mvc_Project.Areas.Admin.ViewModels.Navbar
{
    public class AddViewModel
    {
        public string Name { get; set; }
        public string ToURL { get; set; }
        public int Order { get; set; }
        public bool IsViewOnHeader { get; set; }
        public bool IsViewOnFooter { get; set; }
    }
}
