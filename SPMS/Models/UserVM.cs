namespace CPMS.Models
{
    public class StudentVM
    {
        public string UserId { get; set; }
        public string MatricNo { get; set; }
        public string ImageUrl { get; set; }
        public string? Surname { get; set; }
        public string? OtherNames { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Depeartment { get; set; }
        public IFormFile? File { get; set; }

    }

    public class SupervisorVM
    {
        public string UserId { get; set; }
        public string EmployeeNo { get; set; }
        public string ImageUrl { get; set; }
        public string? Surname { get; set; }
        public string? OtherNames { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Depeartment { get; set; }
        public IFormFile? File { get; set; }




    }
}
