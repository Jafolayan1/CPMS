using Domain.Entities;

namespace CPMS.Models
{
    public class StudentVM
    {
        public int UserId { get; set; }
        public string MatricNo { get; set; }
        public string ImageUrl { get; set; }
        public string? Surname { get; set; }
        public string? OtherNames { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Level { get; set; }

        public int? DepartmentId { get; set; }

        public virtual Department? Department { get; set; }
        public IFormFile? File { get; set; }
    }

    public class SupervisorVM
    {
        public int UserId { get; set; }
        public string EmployeeNo { get; set; }
        public string ImageUrl { get; set; }
        public string? Surname { get; set; }
        public string? OtherNames { get; set; }
        public string? Email { get; set; }
        public string Level { get; set; }

        public string? PhoneNumber { get; set; }
        public int? DepartmentId { get; set; }

        public virtual Department? Department { get; set; }
    }

    public class SignUpVM
    {
        public string EmployeeNo { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
    }
}