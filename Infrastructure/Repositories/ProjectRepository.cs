using Domain.Entities;
using Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
	public class ProjectRepository : GenericRepository<Project>, IProjectRepository
	{
		public ProjectRepository(ApplicationContext context) : base(context)
		{
		}

		public override IEnumerable<Project> Find(Expression<Func<Project, bool>> expression, bool trackchanges)
		{
			return _context.Projects.Include(x => x.Student).Where(expression);
		}

		public override Project GetById(object id)
		{
			return _context.Projects.Include(u => u.Student).Include(d => d.Supervisor).AsNoTracking().FirstOrDefault(x => x.ProjectId.Equals(id));
		}

		public Project GetByMatric(object id)
		{
			return _context.Projects.Include(st => st.Student).Include(su => su.Supervisor).Include(c => c.Chapters).Where(s => s.Status.Equals("Approved")).FirstOrDefault(x => x.Matric.Equals(id));
		}
	}
}