namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISupervisorRepository Supervisors { get; }
        IStudentRepository Students { get; }
        IProjectRepository Projects { get; }
        IDepartmentRepository Departments { get; }
        INotificationRepository Notifications { get; }
        IChapterRepository Chapters { get; }

        Task SaveAsync();

        void Clear();
    }
}