using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class SupervisorRepository : GenericRepository<Supervisor>, ISupervisorRepository
    {
        public SupervisorRepository(ApplicationContext context) : base(context)
        {
        }


    }
}