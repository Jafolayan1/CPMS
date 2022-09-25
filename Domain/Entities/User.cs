using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        public string? Surname { get; set; }
        public string? OtherNames { get; set; }
        public override string? Email { get; set; }
        public override string? PhoneNumber { get; set; }
        [NotMapped]
        public string[] Role { get; set; }

    }
}
