using Domain.Entities;

namespace Domain.Dtos
{
    public class UserDto
    {
        public User User { get; set; }
        public string? Surname { get; set; }
        public string? OtherNames { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public string MatricNo { get; set; }
        public string EmployeeId { get; set; }


    }
}
