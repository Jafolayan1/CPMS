using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationContext context) : base(context)
        {
        }
        public override Department GetById(object id)
        {
            return _context.Departments.FirstOrDefault(x => x.Name.Equals(id));
        }
    }
}