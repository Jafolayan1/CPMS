using Domain.Entities;
using Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
	public class ChapterRepository : GenericRepository<Chapter>, IChapterRepository
	{
		public ChapterRepository(ApplicationContext context) : base(context)
		{
		}

		public override IEnumerable<Chapter> Find(Expression<Func<Chapter, bool>> expression, bool trackchanges)
		{
			return _context.Chapters.Include(u => u.Project).ThenInclude(st => st.Students).ThenInclude(s => s.Supervisor)
				.ThenInclude(d => d.Department).Where(expression).OrderBy(x => x.ChapterId).AsSplitQuery();
		}

		public override Chapter GetById(object id)
		{
			return _context.Chapters.Include(u => u.Project).ThenInclude(st => st.Students).ThenInclude(s => s.Supervisor)
				.ThenInclude(d => d.Department).OrderBy(x => x.ChapterId).AsSplitQuery().FirstOrDefault(x => x.ChapterId.Equals(id));
		}

		public Chapter GetByMatric(string id)
		{
			return _context.Chapters.Include(u => u.Project).ThenInclude(st => st.Students).ThenInclude(s => s.Supervisor).ThenInclude(d => d.Department).OrderBy(x => x.ChapterId).AsSplitQuery().FirstOrDefault(x => x.Matric.Equals(id));
		}
	}
}