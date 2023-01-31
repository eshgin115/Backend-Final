using First_For_Mvc_Project.Areas.Client.ViewModels.Shopping;
using First_For_Mvc_Project.Database.Models;

namespace First_For_Mvc_Project.Services.Concretes
{
    public interface IOrderService
    {
        Task<Order> AddOrderAsync(int SumToal);
        Task AddOrderProductAsync(OrderViewModel model, string OrderId);
    }
}
