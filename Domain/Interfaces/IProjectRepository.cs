using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Project GetByMatric(object id);
    }
}