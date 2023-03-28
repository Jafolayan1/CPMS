namespace Domain.Entities
{
    public class Complaint
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime When { get; set; } = DateTime.Now;
    }
}
