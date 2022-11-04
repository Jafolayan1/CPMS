namespace Domain.Entities
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string Content { get; set; }
        public DateTime When { get; set; } = DateTime.Now;
        public bool IsRead { get; set; }

        public int SupervisorId { get; set; }
        public virtual Supervisor Supervisor { get; set; }

    }
}
