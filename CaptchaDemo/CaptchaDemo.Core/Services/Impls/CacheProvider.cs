using System;
using System.Runtime.Caching;
using CaptchaDemo.Configuration;

namespace CaptchaDemo.Core.Services.Impls
{
	public class CacheProvider : ICacheProvider
	{
		#region Dependencies

		private readonly MemoryCache _memoryCache;
		private readonly ICaptchaConfiguration _captchaConfiguration;

		#endregion

		#region .ctor

		public CacheProvider(ICaptchaConfiguration captchaConfiguration)
		{
			_captchaConfiguration = captchaConfiguration;
			_memoryCache = MemoryCache.Default;
		}

		#endregion

		#region Public Methods

		public void Add(string key, object value)
		{
			_memoryCache.Add(key, value, DateTimeOffset.UtcNow.AddMinutes(_captchaConfiguration.CaptchaLifeTime));
		}

		public object Get(string key)
		{
			return _memoryCache.Contains(key) ? _memoryCache.Get(key) : null;
		}

		public void Delete(string key)
		{
			_memoryCache.Remove(key);
		}

		#endregion
	}
}
