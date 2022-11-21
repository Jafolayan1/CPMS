using AspNetCoreHero.ToastNotification;

using CPMS.Helpers;

using Domain.Interfaces;

using Service;
using Service.Configuration;

namespace CPMS.Extension
{
    public static class ConfigureDependencies
    {
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();

            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IFileHelper, FileHelper>();
            services.AddTransient<IUserAccessor, UserAccessor>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.Configure<ILovePdfSettings>(configuration.GetSection("iLovePdf"));

            services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });
        }
    }
}