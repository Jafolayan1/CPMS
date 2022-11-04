using Domain.Entities;

using Infrastructure.Seed;

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
            builder.ApplyConfiguration(new SeedRoles());
            builder.ApplyConfiguration(new SeedDepartments());

            base.OnModelCreating(builder);
        }

        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Notification> Notifications { get; set; }

    }
}