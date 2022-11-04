using CPMS.Extension;
using CPMS.Hubs;

using Domain.Entities;

using Infrastructure;

using Microsoft.AspNetCore.Identity;

using Service.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mvcBuilder = builder.Services.AddControllersWithViews();
if (builder.Environment.IsDevelopment())
    mvcBuilder.AddRazorRuntimeCompilation();

ConfigureRepositories.AddServices(builder.Services, builder.Configuration);
ConfigureDependencies.AddServices(builder.Services, builder.Configuration);
builder.Services.AddSignalR();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    ctx.Database.EnsureCreated();
    var email = "admin@cpms.com";
    var phone = "090909008744";

    if (!ctx.Users.Any(u => u.Email == email))
    {
        var adminUser = new User
        {
            Surname = "Super ",
            OtherNames = "Admin",
            PhoneNumber = phone,
            Email = email,
            NormalizedEmail = email.ToUpper(),
            UserName = "Admin",
            ImageUrl = "https://cdn-icons-png.flaticon.com/512/3135/3135755.png",
            NormalizedUserName = email,
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        var result = userMgr.CreateAsync(adminUser, "Password").GetAwaiter().GetResult();
        userMgr.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
    }
}

//using (var svp = app.Services.CreateScope())
//{
//    var context = svp.ServiceProvider.GetRequiredService<ApplicationContext>();
//    if (context.Database.GetPendingMigrations().Any())
//        context.Database.Migrate();
//}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<MessageHub>("/messageHub");
app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();