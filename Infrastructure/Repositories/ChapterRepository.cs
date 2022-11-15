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
            return _context.Chapters.Include(x => x.Project).Where(expression);
        }

        public override Chapter GetById(object id)
        {
            return _context.Chapters.Include(u => u.Project).FirstOrDefault(x => x.Matric.Equals(id));
        }
    }
}
