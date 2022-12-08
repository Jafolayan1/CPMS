namespace Domain.Entities
{
	public class Message
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Text { get; set; }
		public DateTime When { get; set; } = DateTime.Now;

		public int UserId { get; set; }
		public virtual User User { get; set; }
	}
}