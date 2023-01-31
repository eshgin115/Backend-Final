using Pronia.Attributs;
using Pronia.Services.Concretes;
using Pronia.Services.Services;

namespace Pronia.Infrastructure.Configurations
{
    public static class RegisterCustomServicesConfigurations
    {
        public static void RegisterCustomServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IFileService, FileService>();

            services.AddScoped<IEmailService, SMTPService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddSingleton<IFileService, FileService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserActivationService, UserActivationService>();
            services.AddScoped<IsAuthenticated>();
        }
    }
}
