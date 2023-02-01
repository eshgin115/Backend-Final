namespace Pronia.Areas.Admin.ViewModels.BlogVideo
{
    public class BlogVideosViewModel
    {
        public int BlogId { get; set; }
        public List<ListItem>? videos { get; set; }





        public class ListItem
        {
            public string? VideoURLFromBrauser { get; set; }
            public int Id { get; set; }
            public string? VideoUrL { get; set; }
            public int? Order { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}
