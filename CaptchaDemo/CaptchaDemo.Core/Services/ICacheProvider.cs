namespace CaptchaDemo.Core.Services
{
	public interface ICacheProvider
	{
		void Add(string key, object value);
		object Get(string key);
		void Delete(string key);
	}
}
