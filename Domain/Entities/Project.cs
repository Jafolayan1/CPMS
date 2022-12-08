using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class Project
	{
		public int ProjectId { get; set; }
		public string Topic { get; set; }
		public string Matric { get; set; }
		public string Status { get; set; }
		public string? Remark { get; set; }
		public IEnumerable<Chapter>? Chapters { get; set; }
		public string FileUrl { get; set; }

		[NotMapped]
		public IFormFile File { get; set; }

		public int StudentId { get; set; }
		public virtual Student Student { get; set; }

		public int? SupervisorId { get; set; }
		public virtual Supervisor Supervisor { get; set; }
		public DateTime DateSubmitted { get; set; } = DateTime.UtcNow;
	}

	public class Chapter
	{
		public int ChapterId { get; set; }
		public ChapterName ChapterName { get; set; }
		public string Matric { get; set; }

		public string Status { get; set; }
		public string FileUrl { get; set; }

		[NotMapped]
		public IFormFile File { get; set; }

		public string? Remark { get; set; }
		public DateTime DateSubmitted { get; set; } = DateTime.UtcNow;
		public int ProjectId { get; set; }
		public virtual Project Project { get; set; }
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