using Pronia.Areas.Admin.Hubs;
using Pronia.Services.Concretes;
using Microsoft.AspNetCore.SignalR;

namespace Pronia.Services.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<AlertHub> _hubContext;
        private readonly IUserService _userService;
        public const string TITLE = "New order created";
        public NotificationService(IHubContext<AlertHub> hubContext, IUserService userService)
        {
            _hubContext = hubContext;
            _userService = userService;
        }
        public async Task SenOrderCreatedToAdmin(string trackingCode)
        {
            string content = $"{_userService.CurrentUser.FirstName} {_userService.CurrentUser.LastName} created new order {trackingCode}";

            await _hubContext.Clients.All.SendAsync("Notify", new { Title = TITLE, Content = content });
        }
    }
}
