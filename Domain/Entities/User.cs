using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        public string? Surname { get; set; }
        public string? OtherNames { get; set; }


    }
}
