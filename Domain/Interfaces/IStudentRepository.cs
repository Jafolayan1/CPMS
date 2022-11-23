using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Student GetByMatric(string id);
    }
}