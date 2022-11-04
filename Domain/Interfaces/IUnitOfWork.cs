namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISupervisorRepository Supervisors { get; }
        IStudentRepository Students { get; }
        IProjectRepository Projects { get; }
        IDepartmentRepository Departments { get; }
        INotificationRepository Notifications { get; }

        Task SaveAsync();
        void Clear();

    }
}