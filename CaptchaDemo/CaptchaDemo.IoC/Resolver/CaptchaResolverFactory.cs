using Autofac;
using Autofac.Integration.Mvc;
using CaptchaDemo.Core.Services;
using CaptchaDemo.Data.Enum;

namespace CaptchaDemo.IoC.Resolver
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

		public T ResolveService<T>()
		{
			return _componentContext.Resolve<T>();
		}
	}
}
