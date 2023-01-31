using First_For_Mvc_Project.Attributs;
using First_For_Mvc_Project.Services.Concretes;
using First_For_Mvc_Project.Services.Services;

namespace First_For_Mvc_Project.Infrastructure.Configurations
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
