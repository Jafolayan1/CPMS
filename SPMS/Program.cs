using Infrastructure;

using Microsoft.EntityFrameworkCore;

using Service.Configuration;

using SPMS.Extension;
using SPMS.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mvcBuilder = builder.Services.AddControllersWithViews();
if (builder.Environment.IsDevelopment())
	mvcBuilder.AddRazorRuntimeCompilation();

ConfigureRepositories.AddServices(builder.Services, builder.Configuration);
ConfigureDependencies.AddServices(builder.Services, builder.Configuration);
builder.Services.AddSignalR();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

using (IServiceScope svp = app.Services.CreateScope())
{
	var context = svp.ServiceProvider.GetRequiredService<ApplicationContext>();
	if (context.Database.GetPendingMigrations().Any())
		context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseCookiePolicy();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
	 name: "areas",
	 pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Account}/{action=Login}/{id?}");

app.MapHub<ChatHub>($"/{nameof(ChatHub)}");

app.Run();