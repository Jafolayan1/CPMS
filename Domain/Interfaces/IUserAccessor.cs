using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserAccessor : IDisposable
    {
        User GetUser();

        Student GetStudent();

        Supervisor GetSupervisor();
    }
}