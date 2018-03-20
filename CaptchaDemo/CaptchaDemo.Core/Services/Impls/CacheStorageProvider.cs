using System;
using CaptchaDemo.Core.Data.Entities;

namespace CaptchaDemo.Core.Services.Impls
{
	public class CacheStorageProvider : ICaptchaStorageProvider
	{
		#region Dependencies

		private readonly ICacheProvider _cacheProvider;
		
		#endregion

		#region .ctor

		public CacheStorageProvider(ICacheProvider cacheProvider)
		{
			_cacheProvider = cacheProvider;
		}

		#endregion

		#region Public Methods

		public Question Get(string id)
		{
			return _cacheProvider.Get(id) as Question;
		}

		public void Delete(string id)
		{
			_cacheProvider.Delete(id);
		}

		public void Insert(Question model)
		{
			_cacheProvider.Add(model.Id, model);
		}

		public string CreateIdentifier()
		{
			return Guid.NewGuid().ToString();
		}

		#endregion
	}
}
