using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            Supervisors = new SupervisorRepository(_context);
            Students = new StudentRepository(_context);
            Projects = new ProjectRepository(_context);
            Departments = new DepartmentRepository(_context);
        }

        public ISupervisorRepository Supervisors { get; private set; }
        public IStudentRepository Students { get; private set; }
        public IProjectRepository Projects { get; private set; }
        public IDepartmentRepository Departments { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}