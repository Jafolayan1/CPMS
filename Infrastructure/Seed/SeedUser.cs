using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Seed
{
    public class SeedUser : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = 1,
                    Surname = "Supervisor",
                    OtherNames = "Supervisor",
                    Email = "supervisor@gmail.com",
                    PhoneNumber = "080765432576",
                    ImageUrl = "",
                    UserName = "EM20200104529",

                });
        }

    }
}
