namespace First_For_Mvc_Project.Areas.Admin.ViewModels.Brand
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; } = null!;

        public IFormFile Photo { get; set; } = null!;
    }
}
