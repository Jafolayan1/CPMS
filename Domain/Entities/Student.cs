namespace Domain.Entities
{
    public class Student : BaseEntity
    {
        public int StudentId { get; set; }
        public string MatricNo { get; set; }
        public Level? Level { get; set; }
        public int? SupervisorId { get; set; }
        public virtual Supervisor Supervisor { get; set; }
        public ICollection<Project> Projects { get; set; }
    }

    public enum Level
    {
        NDI,
        NDII,
        HNDI,
        HNDII,
    }
}