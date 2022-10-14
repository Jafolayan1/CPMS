using Domain.Entities;
using Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationContext context) : base(context)
        {
        }

        public override Student GetById(object id)
        {
            return _context.Students.Include(u => u.User).Include(d => d.Department).Include(s => s.Supervisor).AsNoTracking().FirstOrDefault();
        }
    }
}