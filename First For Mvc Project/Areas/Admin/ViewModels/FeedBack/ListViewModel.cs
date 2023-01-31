namespace First_For_Mvc_Project.Areas.Admin.ViewModels.FeedBack
{
    public class ListViewModel
    {
        public ListViewModel(int id, string profilePhotoUrl, string fullName, string roleName, string content)
        {
            Id = id;
            ProfilePhotoUrl = profilePhotoUrl;
            FullName = fullName;
            RoleName = roleName;
            Content = content;
        }

        public ListViewModel( int id,string profilePhotoUrl, string fullName, string roleName, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            ProfilePhotoUrl = profilePhotoUrl;
            FullName = fullName;
            RoleName = roleName;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        public int Id { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
