using CaptchaDemo.Core.Data.Enum;
using CaptchaDemo.Core.Services;

namespace CaptchaDemo.Core.IoC.Resolver
{
	public interface ICaptchaResolverFactory
	{
		ICapthcaService GetServiceByType(CaptchaTypes type);
		ICaptchaStorageProvider GetStorageProvider(bool isDatabaseAsStorage);
	}
}
