namespace Domain.Entities
{
    public class Supervisor : BaseEntity
    {
        public int SupervisorId { get; set; }
        public string EmployeeNo { get; set; }
        public ICollection<Student>? ProjectStudents { get; set; }
    }
}