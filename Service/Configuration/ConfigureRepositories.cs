﻿using Domain.Entities;

using Domain.Interfaces;

using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Service.Configuration
{
	public static class ConfigureRepositories
	{
		public static void AddServices(IServiceCollection services, IConfiguration config)
		{
			services.AddDbContext<ApplicationContext>(o => o.UseSqlServer(config.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

			services.AddIdentity<User, Role>()
			   .AddEntityFrameworkStores<ApplicationContext>()
			   .AddDefaultTokenProviders();

			services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(1));

			services.Configure<CookiePolicyOptions>(options =>
			{
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});


			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequiredLength = 6;
				options.Password.RequireLowercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.SignIn.RequireConfirmedEmail = false;
				options.SignIn.RequireConfirmedPhoneNumber = false;
			});

			services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddTransient<ISupervisorRepository, SupervisorRepository>();
			services.AddTransient<IStudentRepository, StudentRepository>();
			services.AddTransient<IDepartmentRepository, DepartmentRepository>();
			services.AddTransient<IProjectRepository, ProjectRepository>();
			services.AddTransient<IProjectArchiveRepository, ProjectArchiveRepository>();
			services.AddTransient<IChapterRepository, ChapterRepository>();
			services.AddTransient<INotificationRepository, NotificationRepository>();

			services.AddTransient<IUnitOfWork, UnitOfWork>();
		}
	}
}