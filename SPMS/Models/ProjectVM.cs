using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPMS.Models
{
	public class ProjectVM
	{
		[Required]
		public int ProjectId { get; set; }

		[Required]
		public string Topic { get; set; }

		[Required]
		public string Matric { get; set; }

		[Required]
		public string Status { get; set; }

		public string? Remark { get; set; }

		[Required]
		public string FileUrl { get; set; }

		[NotMapped]
		public IFormFile File { get; set; }

		[Required]
		public int StudentId { get; set; }

		public DateTime DateSubmitted { get; set; } = DateTime.UtcNow;
	}
}