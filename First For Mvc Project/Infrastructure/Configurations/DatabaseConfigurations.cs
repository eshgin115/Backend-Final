using First_For_Mvc_Project.Database;
using Microsoft.EntityFrameworkCore;

namespace First_For_Mvc_Project.Infrastructure.Configurations
{
    public static class DatabaseConfigurations
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("Eshqin-PC"));
            });
        }
    }
}
