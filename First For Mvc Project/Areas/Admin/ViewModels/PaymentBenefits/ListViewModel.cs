namespace First_For_Mvc_Project.Areas.Admin.ViewModels.PaymentBenefits
{
    public class ListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public string Content { get; set; }
        public string ImageURL { get; set; }
        public ListViewModel(int id, string title, int order, string content, string imageURL)
        {
            Id = id;
            Title = title;
            Order = order;
            Content = content;
            ImageURL = imageURL;
        }
    }
}
