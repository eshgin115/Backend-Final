using System.ComponentModel.DataAnnotations;

namespace First_For_Mvc_Project.Areas.Admin.ViewModels.FeedBack
{
    public class AddViewModel
    {
        [Required]
        public IFormFile ProfilePhoto { get; set; }

        [Required]
        public string Name { get; set; }

      

        [Required]
        public string Content { get; set; }

        public List<ItemViewModel>? Roles { get; set; }
        public int RoleId { get; set; }
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
