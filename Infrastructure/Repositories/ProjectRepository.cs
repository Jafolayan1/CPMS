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
            return _context.Projects.Include(x => x.User).Where(expression);
        }
    }
}
