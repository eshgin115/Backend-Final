using AspNetCore.IServiceCollection.AddIUrlHelper;
using First_For_Mvc_Project.Infrastructure.Configurations;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace First_For_Mvc_Project.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.Cookie.Name = "Identity";
                    o.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    o.LoginPath = "/auth/login";
                    o.AccessDeniedPath = "/admin/auth/login";
                });

            services.AddHttpContextAccessor();

            services.AddUrlHelper();
            services.AddSignalR();

            services.ConfigureMvc();

            services.ConfigureDatabase(configuration);

            services.ConfigureOptions(configuration);

            services.ConfigureFluentValidatios(configuration);

            services.RegisterCustomServices(configuration);

        }
    }
}
