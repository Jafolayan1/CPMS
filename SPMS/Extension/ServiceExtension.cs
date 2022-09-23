using Domain.Entities;
using Domain.Interfaces;

using Infrastructure;
using Infrastructure.Repositories;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CPMS.Extension
{
    public static class ServiceExtension
    {
        public static void ConfigureRepositories(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationContext>(o => o.UseSqlServer(config.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationContext>()
               .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });



            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<ISupervisorRepository, SupervisorRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
        }
    }
}
