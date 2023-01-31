using Pronia.Areas.Client.ViewModels.Shopping;
using Pronia.Database.Models;

namespace Pronia.Services.Concretes
{
    public interface IOrderService
    {
        Task<Order> AddOrderAsync(int SumToal);
        Task AddOrderProductAsync(OrderViewModel model, string OrderId);
    }
}
