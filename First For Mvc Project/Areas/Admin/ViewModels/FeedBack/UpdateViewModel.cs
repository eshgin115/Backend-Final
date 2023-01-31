namespace First_For_Mvc_Project.Areas.Admin.ViewModels.FeedBack
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        public string ProfilePhotoUrl { get; set; } = null!;

        public IFormFile ProfilePhoto { get; set; } = null!;

        public string? Name { get; set; }


        public string? Content { get; set; }

        public List<ItemViewModel>? Roles { get; set; }
        public int? RoleId { get; set; }
        public class ItemViewModel 
        {
            public ItemViewModel(int id, string? name)
            {
                Id = id;
                Name = name;
            }

            public int Id { get; set; }
            public string Name { get; set; } = null!;
        }

    }
}
