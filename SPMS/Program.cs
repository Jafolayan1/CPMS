using Domain.Entities;

using Service.Configuration;

using SPMS.Extension;
using SPMS.Helpers;
using SPMS.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureRepositories.AddServices(builder.Services, builder.Configuration);
ConfigureDependencies.AddServices(builder.Services, builder.Configuration);
builder.Services.AddSignalR();
builder.Services.AddProgressiveWebApp();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(12);
    options.Cookie.IsEssential = true;
});

//builder.Services.AddRazorPages();


var mvcBuilder = builder.Services.AddControllersWithViews(); //.AddNewtonsoftJson(options =>
//{
//    // Use the default property (Pascal) casing
//    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
//}); 
if (builder.Environment.IsDevelopment())
    mvcBuilder.AddRazorRuntimeCompilation();


var settings = builder.Configuration.GetSection("Syncfussion").Get<Syncfussion>();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(settings.Key);

builder.Services.AddScoped<FileHelper>();

var app = builder.Build();

//using (IServiceScope svp = app.Services.CreateScope())
//{
//	var context = svp.ServiceProvider.GetRequiredService<ApplicationContext>();
//	if (context.Database.GetPendingMigrations().Any())
//		context.Database.Migrate();
//}

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
//app.MapRazorPages();

app.Run();