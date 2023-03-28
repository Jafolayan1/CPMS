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

        public IEnumerable<Department> GetDepartments()
        {
            var departments = _context.Departments.ToList();
            departments.Insert(0, new Department { DepartmentId = 0, Name = " --Select Department-- " });
            return departments;
        }

    }
}