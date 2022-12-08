using System.Linq.Expressions;

namespace Domain.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		T GetById(object id);

		IEnumerable<T> GetAll();

		IEnumerable<T> Find(Expression<Func<T, bool>> expression, bool trackchanges);

		void Add(T entity);

		void Update(T entity);

		void AddRange(IEnumerable<T> entities);

		void Remove(T entity);

		void RemoveRange(IEnumerable<T> entities);
	}
}