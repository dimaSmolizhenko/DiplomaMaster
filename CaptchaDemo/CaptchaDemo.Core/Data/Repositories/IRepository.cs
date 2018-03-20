using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CaptchaDemo.Core.Data.Repositories
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
		string CreateObjectId();
		string SaveFile(Stream stream, string fileName);
		void SaveFile(string id, Stream stream, string fileName);
		string GetFile(string id);
	}
}
