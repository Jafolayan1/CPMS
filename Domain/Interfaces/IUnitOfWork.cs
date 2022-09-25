namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISupervisorRepository Supervisors { get; }
        IStudentRepository Students { get; }
        Task SaveAsync();

    }
}
