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
			return _context.Chapters.Include(u => u.Project).ThenInclude(st => st.Student).ThenInclude(s => s.Supervisor)
				.ThenInclude(d => d.Department).Where(expression);
		}
		public override Chapter GetById(object id)
		{
			return _context.Chapters.Include(u => u.Project).ThenInclude(st => st.Student).ThenInclude(s => s.Supervisor)
				.ThenInclude(d => d.Department).AsNoTracking().FirstOrDefault(x => x.ChapterId.Equals(id));
		}
		public Chapter GetByMatric(string id)
		{
			return _context.Chapters.Include(u => u.Project).ThenInclude(st => st.Student).ThenInclude(s => s.Supervisor)
				.ThenInclude(d => d.Department).FirstOrDefault(x => x.Matric.Equals(id));
		}
	}
}