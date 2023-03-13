using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Project : BaseProjectClass
    {
        public int ProjectId { get; set; }
        public string Status { get; set; }
        public string Topic { get; set; }

        public string? FileData { get; set; }
        public string? Remark { get; set; }

        public virtual ICollection<Chapter>? Chapters { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }

    public class Chapter : BaseProjectClass
    {
        public int ChapterId { get; set; }
        public string Status { get; set; }
        public string Topic { get; set; }
        public string? Remark { get; set; }

        public ChapterName ChapterName { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }

    public class ProjectArchive : BaseProjectClass
    {
        public int ProjectArchiveId { get; set; }
        public string Title { get; set; }

        public string ProjectCode { get; set; }
        public string CaseStudy { get; set; }
        public string Year { get; set; }

        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public ICollection<Student> Students { get; set; }
    }

    public class BaseProjectClass
    {
        [NotMapped]
        public string Matric { get; set; }

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