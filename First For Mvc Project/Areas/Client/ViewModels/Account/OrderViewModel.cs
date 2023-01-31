namespace Pronia.Areas.Client.ViewModels.Account
{
    public class OrderViewModel
    {
        public OrderViewModel(string identifikator, DateTime orderDate, string currentStatus, int totalPrice, int? orderProductsCount)
        {

            Identifikator = identifikator;
            OrderDate = orderDate;
            CurrentStatus = currentStatus;
            TotalPrice = totalPrice;
            OrderProductsCount = orderProductsCount;
        }


        public string Identifikator { get; set; }
        public DateTime OrderDate { get; set; }
        public string CurrentStatus { get; set; }
        public int TotalPrice { get; set; }
        public int? OrderProductsCount { get; set; }
    }
}
