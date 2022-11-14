using Domain.Entities;

namespace CPMS.Hubs
{
    public interface IMessageHubClient
    {
        Task ReceiveMessage(string message);

        Task ReceiveMessage(Message message);
    }
}