﻿using Pronia.Options;

namespace Pronia.Infrastructure.Configurations
{
    public static class OptionConfigurations
    {
        public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            //var emailConfig = configuration.GetSection("EmailConfiguration").Get<EmailConfigOptions>();
            //services.AddSingleton(emailConfig);

            services.Configure<EmailConfigOptions>(configuration.GetSection(nameof(EmailConfigOptions)));
        }
    }
}
