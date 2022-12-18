namespace Domain.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		ISupervisorRepository Supervisors { get; }
		IStudentRepository Students { get; }
		IProjectRepository Projects { get; }
		IProjectArchiveRepository ProjectArchive { get; }
		IDepartmentRepository Departments { get; }
		INotificationRepository Notifications { get; }
		IChapterRepository Chapters { get; }
		IMessageRepository Messages { get; }

		Task SaveAsync();

		void Clear();
	}
}