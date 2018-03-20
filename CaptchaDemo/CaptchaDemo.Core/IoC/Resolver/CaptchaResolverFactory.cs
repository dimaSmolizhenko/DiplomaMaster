using Autofac;
using Autofac.Integration.Mvc;
using CaptchaDemo.Core.Data.Enum;
using CaptchaDemo.Core.Services;

namespace CaptchaDemo.Core.IoC.Resolver
{
	public class CaptchaResolverFactory : ICaptchaResolverFactory
	{
		private readonly IComponentContext _componentContext;

		public CaptchaResolverFactory()
		{
			_componentContext = AutofacDependencyResolver.Current.ApplicationContainer;
		}

		public ICapthcaService GetServiceByType(CaptchaTypes type)
		{
			return _componentContext.ResolveKeyed<ICapthcaService>(type);
		}

		public ICaptchaStorageProvider GetStorageProvider(bool isDatabaseAsStorage)
		{
			return _componentContext.ResolveKeyed<ICaptchaStorageProvider>(isDatabaseAsStorage);
		}
	}
}
