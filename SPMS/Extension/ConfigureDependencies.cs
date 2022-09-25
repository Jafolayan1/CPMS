using CPMS.Helpers;

using Domain.Interfaces;

using Service;


namespace CPMS.Extension
{
    public static class ConfigureDependencies
    {
        public static void AddServices(IServiceCollection services)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();

            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IFileHelper, FileHelper>();
            services.AddTransient<IUserAccessor, UserAccessor>();

        }
    }
}
