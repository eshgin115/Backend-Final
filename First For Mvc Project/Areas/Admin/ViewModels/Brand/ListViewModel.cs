namespace First_For_Mvc_Project.Areas.Admin.ViewModels.Brand
{
    public class ListViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
   

        public ListViewModel(int id, string imageUrl, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            ImageUrl = imageUrl;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public ListViewModel(int id, string imageUrl)
        {
            Id = id;
            ImageUrl = imageUrl;
        }
    }
}
