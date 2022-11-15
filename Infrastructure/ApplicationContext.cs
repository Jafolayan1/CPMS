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
                Surname = "Super ",
                OtherNames = "Admin",
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

            User supervisor = new()
            {
                Id = 2,
                Surname = "Uthman",
                OtherNames = "Adekunle Adewale",
                UserName = "EM20200104321",
                NormalizedUserName = "EM20200104321",
                Email = "afolayan.oluwatosin20@gmail.com",
                NormalizedEmail = "AFOLAYAN.OLUWATOSIN20@GMAIL.COM",
                PhoneNumber = "1234567890",
                LockoutEnabled = false,
                ImageUrl = "https://cdn-icons-png.flaticon.com/512/3135/3135755.png",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            Supervisor sup = new()
            {
                SupervisorId = 1,
                Surname = "Uthman",
                OtherNames = "Adekunle Adewale",
                UserId = supervisor.Id,
                EmployeeNo = "EM20200104321",
                Email = "afolayan.oluwatosin20@gmail.com",
                DepartmentId = 1,
                PhoneNumber = "1234567890",
                ImageUrl = "https://cdn-icons-png.flaticon.com/512/3135/3135755.png",
            };

            PasswordHasher<User> passwordHasher = new();
            admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin*123");
            supervisor.PasswordHash = passwordHasher.HashPassword(supervisor, "Sup*123");

            builder.Entity<User>().HasData(admin);
            builder.Entity<User>().HasData(supervisor);
            builder.Entity<Supervisor>().HasData(sup);
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
                    new IdentityUserRole<int>() { RoleId = 1, UserId = 1 },
                    new IdentityUserRole<int>() { RoleId = 2, UserId = 2 }
                );
        }

        public static void SeedDeepartment(ModelBuilder builder)
        {
            builder.Entity<Department>().HasData(
                 new Department
                 {
                     DepartmentId = 1,
                     Name = "Computer Science",
                 },
                 new Department
                 {
                     DepartmentId = 2,
                     Name = "Computer Engineering",
                 },
                  new Department
                  {
                      DepartmentId = 3,
                      Name = "Statistics",
                  });
        }
    }
}