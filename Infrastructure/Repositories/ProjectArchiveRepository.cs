using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
	public class ProjectArchiveRepository : GenericRepository<ProjectArchive>, IProjectArchiveRepository
	{
		public ProjectArchiveRepository(ApplicationContext context) : base(context)
		{
		}
	}
}