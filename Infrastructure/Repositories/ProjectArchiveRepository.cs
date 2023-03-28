using Domain.Entities;
using Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProjectArchiveRepository : GenericRepository<ProjectArchive>, IProjectArchiveRepository
    {
        public ProjectArchiveRepository(ApplicationContext context) : base(context)
        {
        }

        public override IQueryable<ProjectArchive> GetAll()
        {
            return _context.ProjectArchive.Include(u => u.Students).Include(d => d.Department).Include(s => s.Supervisor).AsSplitQuery().OrderBy(o => o.Title);
        }
    }
}