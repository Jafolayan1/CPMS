namespace Domain.Entities
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public ICollection<Supervisor>? Supervisors { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}