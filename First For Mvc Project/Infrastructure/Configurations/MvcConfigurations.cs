namespace First_For_Mvc_Project.Infrastructure.Configurations
{
    public static class MvcConfigurations
    {
        public static void ConfigureMvc(this IServiceCollection services)
        {
            services
               .AddMvc()
               .AddRazorRuntimeCompilation();
        }
    }
}
