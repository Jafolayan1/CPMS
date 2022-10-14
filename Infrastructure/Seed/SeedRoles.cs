using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Seed
{
    public class SeedRoles : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role
                {
                    Id = 1,
                    Name = "Student",
                    NormalizedName = "STUDENT",
                    ConcurrencyStamp = new Guid().ToString()
                },
                 new Role
                 {
                     Id = 2,

                     Name = "Supervisor",
                     NormalizedName = "SUPERVISOR",
                     ConcurrencyStamp = new Guid().ToString()
                 },
                  new Role
                  {
                      Id = 3,
                      Name = "Admin",
                      NormalizedName = "ADMIN",
                      ConcurrencyStamp = new Guid().ToString()
                  });
        }
    }
}