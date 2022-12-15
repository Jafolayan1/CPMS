namespace CPMS.Hubs
{
    public interface IChatHubClient
    {
        Task ReceiveMessage(string message);
    }
}