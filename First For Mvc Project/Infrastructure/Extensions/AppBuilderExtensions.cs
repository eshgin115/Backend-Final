using First_For_Mvc_Project.Areas.Admin.Hubs;

namespace First_For_Mvc_Project.Infrastructure.Extensions
{
    public static class AppBuilderExtensions
    {
        public static void ConfigureMiddlewarePipeline(this WebApplication app)
        {
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{area=exists}/{controller=home}/{action=index}");
            app.MapHub<AlertHub>("hubs/alert-hub");
        }
    }
}
