using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class Student : BaseEntity
	{
		public int StudentId { get; set; }
		public int UserId { get; set; }

		[ForeignKey("UserId")]
		public virtual User User { get; set; }

		public string MatricNo { get; set; }
		public string Level { get; set; }
		public int? SupervisorId { get; set; }
		public virtual Supervisor? Supervisor { get; set; }
		public ICollection<Project> Projects { get; set; }
	}
}