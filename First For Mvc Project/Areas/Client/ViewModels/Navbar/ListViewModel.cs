namespace First_For_Mvc_Project.Areas.Client.ViewModels.Navbar
{
    public class ListViewModel
    {
        public ListViewModel(string name, string uRL, List<SubnavbarItem> subnavbarItems)
        {
            Name = name;
            URL = uRL;
            SubnavbarItems = subnavbarItems;
        }

        public string Name { get; set; }
        public string URL { get; set; }
        public List<SubnavbarItem> SubnavbarItems { get; set; }
       
        public class SubnavbarItem
        {
            public SubnavbarItem(string name, string toURL)
            {
                Name = name;
                ToURL = toURL;
            }

            public string Name { get; set; }
            public string ToURL { get; set; }
        }
    }
}
