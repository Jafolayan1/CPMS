using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class Supervisor : BaseEntity
	{
		public int SupervisorId { get; set; }
		public string FileNo { get; set; }
		public int? UserId { get; set; }

		[ForeignKey("UserId")]
		public virtual User? User { get; set; }

		public ICollection<Student>? ProjectStudents { get; set; }
	}
}