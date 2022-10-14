namespace Domain.Entities
{
    public class Supervisor : BaseEntity
    {
        public string EmployeeNo { get; set; }
        public IEnumerable<Student>? ProjectStudents { get; set; }
    }
}