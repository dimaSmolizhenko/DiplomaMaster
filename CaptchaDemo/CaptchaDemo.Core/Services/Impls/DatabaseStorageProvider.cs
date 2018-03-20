using System.Threading.Tasks;
using CaptchaDemo.Core.Data.Entities;
using CaptchaDemo.Core.Data.Repositories;

namespace CaptchaDemo.Core.Services.Impls
{
	public class DatabaseStorageProvider : ICaptchaStorageProvider
	{
		#region Dependencies

		private readonly IRepository<Question> _repository;

		#endregion

		#region .ctor

		public DatabaseStorageProvider(IRepository<Question> repository)
		{
			_repository = repository;
		}

		#endregion

		#region Public Methods

		public Question Get(string id)
		{
			return Task.Run(async () => await _repository.GetByIdAsync(id)).Result;
		}

		public void Delete(string id)
		{
			Task.Run(async () => await _repository.DeleteAsync(id));
		}

		public void Insert(Question model)
		{
			Task.Run(async () => await _repository.InsertAsync(model)).Wait();
		}

		public string CreateIdentifier()
		{
			return _repository.CreateObjectId();
		}

		#endregion
	}
}
