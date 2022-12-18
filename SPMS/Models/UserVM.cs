using Domain.Entities;

using Microsoft.Build.Framework;

namespace SPMS.Models
{
	public class StudentVM
	{
		public int UserId { get; set; }

		[Required]
		public string MatricNo { get; set; }

		[Required]
		public string ImageUrl { get; set; }

		[Required]
		public string? FullName { get; set; }

		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }

		[Required]
		public string Level { get; set; }

		public int? DepartmentId { get; set; }

		public virtual Department? Department { get; set; }
		public IFormFile? File { get; set; }
	}

	public class SupervisorVM
	{
		public int UserId { get; set; }

		[Required]
		public string FileNo { get; set; }

		[Required]
		public string ImageUrl { get; set; }

		public string? FullName { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public int? DepartmentId { get; set; }

		public virtual Department? Department { get; set; }
	}
}