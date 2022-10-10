namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISupervisorRepository Supervisors { get; }
        IStudentRepository Students { get; }
        IProjectRepository Projects { get; }
        Task SaveAsync();

    }
}
