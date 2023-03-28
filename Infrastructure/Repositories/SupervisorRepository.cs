using Domain.Entities;
using Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SupervisorRepository : GenericRepository<Supervisor>, ISupervisorRepository
    {
        public SupervisorRepository(ApplicationContext context) : base(context)
        {
        }

        public override Supervisor GetById(object id)
        {
            return _context.Supervisors.Include(u => u.User).Include(d => d.Department).Include(s => s.ProjectStudents)
                .ThenInclude(s => s.Projects).AsNoTracking().OrderBy(o => o.FullName).FirstOrDefault(x => x.SupervisorId.Equals(id));
        }

        public Supervisor GetByFileNo(string id)
        {
            return _context.Supervisors.Include(u => u.User).Include(d => d.Department).Include(s => s.ProjectStudents)
                .ThenInclude(s => s.Projects).OrderBy(o => o.FullName).AsNoTracking().FirstOrDefault(x => x.FileNo.Equals(id));
        }

        public override IQueryable<Supervisor> GetAll()
        {
            return _context.Supervisors.Include(u => u.User).Include(d => d.Department).Include(s => s.ProjectStudents).ThenInclude(s => s.Projects).AsNoTracking().OrderBy(o => o.FullName);
        }
    }
}