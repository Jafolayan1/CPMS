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

        public override IQueryable<Project> GetAll()
        {
            return _context.Projects.Include(st => st.Students).Include(su => su.Supervisor).Include(c => c.Chapters).AsSplitQuery();
        }

        public override IQueryable<Project> Find(Expression<Func<Project, bool>> expression, bool trackchanges)
        {
            return _context.Projects.Include(x => x.Students).Include(c => c.Chapters).Where(expression).OrderBy(x => x.ProjectId).AsSplitQuery();
        }

        public override Project GetById(object id)
        {
            return _context.Projects.Include(u => u.Students).Include(d => d.Supervisor).OrderBy(x => x.ProjectId).AsSplitQuery().FirstOrDefault(x => x.ProjectId == (int)id);
        }

        public Project GetByMatric(object id)
        {
            return _context.Projects.Include(st => st.Students).Include(su => su.Supervisor).Include(c => c.Chapters).Where(s => s.Status.Equals("Approved")).OrderBy(x => x.ProjectId).AsSplitQuery().FirstOrDefault(x => x.SupervisorId.Equals(id));
        }
    }
}