using Domain.Entities;

using Infrastructure.Seed;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new SeedRoles());
            base.OnModelCreating(builder);
        }


        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}