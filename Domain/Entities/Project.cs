using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class Project : BaseProjectClass
	{
		public int ProjectId { get; set; }

		public ICollection<Chapter>? Chapters { get; set; }
		public virtual ICollection<Student> Student { get; set; }
	}

	public class Chapter : BaseProjectClass
	{
		public int ChapterId { get; set; }
		public ChapterName ChapterName { get; set; }

		public int ProjectId { get; set; }
		public virtual Project Project { get; set; }
	}

	public class CompleteProject : BaseProjectClass
	{
		public int CompleteProjectId { get; set; }
	}

	public class BaseProjectClass
	{
		public string Topic { get; set; }
		[NotMapped]
		public string Matric { get; set; }
		public string Status { get; set; }
		public string? Remark { get; set; }
		public string FileUrl { get; set; }

		[NotMapped]
		public IFormFile File { get; set; }

		public DateTime DateSubmitted { get; set; } = DateTime.UtcNow;

		public int? SupervisorId { get; set; }
		public virtual Supervisor Supervisor { get; set; }
	}

	public enum ChapterName : short
	{
		CHAPTER_1,
		CHAPTER_2,
		CHAPTER_3,
		CHAPTER_4,
		CHAPTER_5,
	}
}