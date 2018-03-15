using CaptchaDemo.Data.Entities;

namespace CaptchaDemo.Core.Services
{
	public interface ICaptchaStorageProvider
	{
		Question Get(string id);
		void Delete(string id);
		void Insert(Question model);
		string CreateIdentifier();
	}
}
