using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        IEnumerable<Department> GetDepartments();
    }
}