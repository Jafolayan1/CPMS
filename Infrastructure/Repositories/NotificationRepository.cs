using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(ApplicationContext context) : base(context)
        {
        }


    }
}
