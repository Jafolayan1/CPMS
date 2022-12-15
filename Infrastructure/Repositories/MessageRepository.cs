using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
	public class MessageRepository : GenericRepository<Message>, IMessageRepository
	{
		public MessageRepository(ApplicationContext context) : base(context)
		{
		}
	}
}
