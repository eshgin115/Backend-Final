using First_For_Mvc_Project.Areas.Client.ViewModels.Shopping;
using First_For_Mvc_Project.Contracts.Order;
using First_For_Mvc_Project.Database;
using First_For_Mvc_Project.Database.Models;
using First_For_Mvc_Project.Services.Concretes;
using Microsoft.EntityFrameworkCore;

namespace First_For_Mvc_Project.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private const int MIN_RANDOM_NUMBER = 10000;
        private const int MAX_RANDOM_NUMBER = 100000;
        private const string PREFIX = "OR";


        public OrderService(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }
        public async Task<Order> AddOrderAsync(int SumToal)
        {
            ArgumentNullException.ThrowIfNull(SumToal);


            var order = new Order
            {
                Id = GenerateId(),
                SumTotalPrice = SumToal,
                User = _userService.CurrentUser,
                Status = Status.Created,

            };
            await _dataContext.Orders.AddAsync(order);
            return order;
        }


        public async Task AddOrderProductAsync(OrderViewModel model, string OrderId)
        {
            ArgumentNullException.ThrowIfNull(model);
            ArgumentNullException.ThrowIfNull(OrderId);
            foreach (var item in model.Models)
            {

                var orderProduct = new OrderProduct
                {
                    PlantId = item.Id,
                    Orderİd = OrderId,
                    Quantity = item.Quantity,
                    SizeId = item.SizeId != null ? item.SizeId : _dataContext.Plants.Include(p=>p.PlantSizes).Where(p => p.Id == item.Id).First().PlantSizes!.First().SizeId,
                    ColorId = item.ColorId != null ? item.ColorId : _dataContext.Plants.Include(p=>p.PlantColors).Where(p => p.Id == item.Id).First().PlantColors!.First().ColorId,
                };
                await _dataContext.OrderProducts.AddAsync(orderProduct);
            }
        }
        private string GenerateNumber()
        {
            Random random = new Random();
            return random.Next(MIN_RANDOM_NUMBER, MAX_RANDOM_NUMBER).ToString();
        }

        private string GenerateId()
        {
            var Id = string.Empty;
            while (true)
            {
                Id = $"{PREFIX}{GenerateNumber()}";

                if (!_dataContext.Orders.Any(o => o.Id == Id))
                {
                    return Id;
                }
            }
        }
    }
}
