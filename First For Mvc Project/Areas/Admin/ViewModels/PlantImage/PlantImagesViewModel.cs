namespace First_For_Mvc_Project.Areas.Admin.ViewModels.PlantImage
{
    public class BlogImagesViewModel
    {
        public int PlantId { get; set; }
        public List<ListItem>? Images { get; set; }





        public class ListItem
        {
            public int Id { get; set; }
            public string? ImageUrL { get; set; }
            public int? Order { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}
