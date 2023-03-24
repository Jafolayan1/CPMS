using Domain.Entities;

namespace Domain.Interfaces
{
	public interface IUserAccessor
	{
		User GetUser();

		Student GetStudent();

		Supervisor GetSupervisor();
	}
}