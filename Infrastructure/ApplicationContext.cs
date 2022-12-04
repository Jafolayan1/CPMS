using Domain.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedUsers(builder);
            SeedRoles(builder);
            SeedUserRoles(builder);
            SeedDeepartment(builder);
        }

        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        private static void SeedUsers(ModelBuilder builder)
        {
            User admin = new()
            {
                Id = 1,
                FullName = " Super Admin",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                PhoneNumber = "1234567890",
                LockoutEnabled = false,
                ImageUrl = "https://cdn-icons-png.flaticon.com/512/3135/3135755.png",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            PasswordHasher<User> passwordHasher = new();
            admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin*123");

            builder.Entity<User>().HasData(admin);
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
                new Role() { Id = 1, Name = "Admin", ConcurrencyStamp = new Guid().ToString(), NormalizedName = "ADMIN" },
                new Role() { Id = 2, Name = "Supervisor", ConcurrencyStamp = new Guid().ToString(), NormalizedName = "SUPERVISOR" },
                new Role() { Id = 3, Name = "Student", ConcurrencyStamp = new Guid().ToString(), NormalizedName = "STUDENT" }

                );
        }

        private static void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<int>>().HasData
                (
                    new IdentityUserRole<int>() { RoleId = 1, UserId = 1 }
                );
        }

        public static void SeedDeepartment(ModelBuilder builder)
        {
            builder.Entity<Department>().HasData(
                 new Department
                 {
                     DepartmentId = 1,
                     Name = "Computer Science",
                 });
        }
    }
}