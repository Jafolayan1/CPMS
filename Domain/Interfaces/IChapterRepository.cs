using Domain.Entities;

namespace Domain.Interfaces
{
	public interface IChapterRepository : IGenericRepository<Chapter>
	{
		Chapter GetByMatric(string matricId);
	}
}