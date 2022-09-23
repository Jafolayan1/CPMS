using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
