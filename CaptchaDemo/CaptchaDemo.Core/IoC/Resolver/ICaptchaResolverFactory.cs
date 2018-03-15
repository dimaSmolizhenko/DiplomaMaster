using CaptchaDemo.Core.Services;
using CaptchaDemo.Data.Enum;

namespace CaptchaDemo.Core.IoC.Resolver
{
	public interface ICaptchaResolverFactory
	{
		ICapthcaService GetServiceByType(CaptchaTypes type);
		ICaptchaStorageProvider GetStorageProvider(bool isDatabaseAsStorage);
	}
}
