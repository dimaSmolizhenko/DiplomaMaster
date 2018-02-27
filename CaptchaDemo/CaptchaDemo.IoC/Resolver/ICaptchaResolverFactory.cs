using CaptchaDemo.Core.Services;
using CaptchaDemo.Data.Enum;

namespace CaptchaDemo.IoC.Resolver
{
	public interface ICaptchaResolverFactory
	{
		ICapthcaService GetServiceByType(CaptchaTypes type);
	}
}
