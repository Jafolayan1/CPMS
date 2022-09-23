using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Seed
{
    public class SeedRoles : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = "Student",
                    NormalizedName = "STUDENT",
                    ConcurrencyStamp = new Guid().ToString()
                },
                 new IdentityRole
                 {
                     Name = "Supervisor",
                     NormalizedName = "SUPERVISOR",
                     ConcurrencyStamp = new Guid().ToString()
                 },
                  new IdentityRole
                  {
                      Name = "Admin",
                      NormalizedName = "ADMIN",
                      ConcurrencyStamp = new Guid().ToString()
                  });
        }
    }
}
