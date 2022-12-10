using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
	public class CompleteProjectRepository : GenericRepository<CompleteProject>, ICompleteProjectRepository
	{
		public CompleteProjectRepository(ApplicationContext context) : base(context)
		{
		}
	}
}