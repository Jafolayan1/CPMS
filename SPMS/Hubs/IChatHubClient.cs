namespace SPMS.Hubs
{
	public interface IChatHubClient
	{
		Task ReceiveMessage(string message);
	}
}