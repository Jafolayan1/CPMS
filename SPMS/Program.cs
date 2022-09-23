using CPMS.Extension;

using Domain.Entities;

using Infrastructure;

using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.ConfigureRepositories(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    ctx.Database.EnsureCreated();
    var email = "admin@spms.com";
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
            UserName = phone,
            NormalizedUserName = email,
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        var result = userMgr.CreateAsync(adminUser, "Pa$$word").GetAwaiter().GetResult();
        userMgr.AddToRoleAsync(adminUser, "SAdmin").GetAwaiter().GetResult();
    }
}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
