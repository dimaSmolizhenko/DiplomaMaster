using Autofac;
using CaptchaDemo.Core.Services;
using CaptchaDemo.Data.Enum;

namespace CaptchaDemo.IoC.Resolver
{
	public class CaptchaResolverFactory : ICaptchaResolverFactory
	{
		private readonly IContainer _container;

		public CaptchaResolverFactory(IContainer container)
		{
			_container = container;
		}

		public ICapthcaService GetServiceByType(CaptchaTypes type)
		{
			return _container.ResolveKeyed<ICapthcaService>(type);
		}
	}
}
