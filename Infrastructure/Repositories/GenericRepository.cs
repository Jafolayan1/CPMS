using Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		protected readonly ApplicationContext _context;

		public GenericRepository(ApplicationContext context)
		{
			_context = context;
		}

		public void Add(T entity)
		{
			_context.Set<T>().Add(entity);
		}

		public void AddRange(IEnumerable<T> entities)
		{
			_context.Set<T>().AddRange(entities);
		}

		public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression, bool trackchanges)
		{
			return !trackchanges ? _context.Set<T>().AsNoTracking().Where(expression).ToList() : _context.Set<T>().Where(expression).ToList();
		}

		public virtual IEnumerable<T> GetAll()
		{
			return _context.Set<T>().ToList();
		}

		public virtual T GetById(object id)
		{
			return _context.Set<T>().Find(id);
		}

		public void Remove(T entity)
		{
			_context.Set<T>().Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entities)
		{
			_context.Set<T>().RemoveRange(entities);
		}

		public void Update(T entity)
		{
			_context.Set<T>().Update(entity);
		}
	}
}