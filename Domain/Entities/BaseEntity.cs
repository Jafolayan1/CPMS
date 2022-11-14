using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class BaseEntity
    {
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public string? Surname { get; set; }
        public string? OtherNames { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? DepartmentId { get; set; }

        [DisplayFormat(NullDisplayText = " ")]
        public Department? Department { get; set; }

        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }

        [NotMapped]
        public string? FullName
        { get { return $"{Surname}  {OtherNames}"; } }
    }
}