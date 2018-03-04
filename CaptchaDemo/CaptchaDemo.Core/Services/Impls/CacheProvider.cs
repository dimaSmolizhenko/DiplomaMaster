using System;
using System.Runtime.Caching;

namespace CaptchaDemo.Core.Services.Impls
{
	public class CacheProvider : ICacheProvider
	{
		private readonly MemoryCache _memoryCache;

		public CacheProvider()
		{
			_memoryCache = MemoryCache.Default;
		}

		public void Add(string key, object value)
		{
			_memoryCache.Add(key, value, DateTimeOffset.UtcNow.AddMinutes(30));
		}

		public object Get(string key)
		{
			return _memoryCache.Contains(key) ? _memoryCache.Get(key) : null;
		}
	}
}
