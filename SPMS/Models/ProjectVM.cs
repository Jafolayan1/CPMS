using System.ComponentModel.DataAnnotations.Schema;

namespace CPMS.Models
{
    public class ProjectVM
    {
        public int ProjectId { get; set; }
        public string Topic { get; set; }
        public string Matric { get; set; }
        public string Status { get; set; }
        public string? Remark { get; set; }
        public string FileUrl { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public int StudentId { get; set; }
        public DateTime DateSubmitted { get; set; } = DateTime.UtcNow;
    }
}
