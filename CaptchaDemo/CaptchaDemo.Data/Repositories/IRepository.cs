using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaptchaDemo.Data.Repositories
{
	public interface IRepository<T>
	{
		Task<T> GetByIdAsync(string id);
		Task<IEnumerable<T>> GetAllAsync();
		Task InsertAsync(T entity);
		Task InsertManyAsync(IEnumerable<T> entities);
		Task UpdateAsync(T entity);
		Task DeleteAsync(string id);
		Task<IList<T>> GetByTypeAsync(string type);
	}
}
