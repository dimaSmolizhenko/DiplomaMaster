using System;
using System.Configuration;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using CaptchaDemo.Core.Configuration;
using CaptchaDemo.Core.Configuration.Consts;
using CaptchaDemo.Core.Configuration.Impls;
using CaptchaDemo.Core.Data.Consts;
using CaptchaDemo.Core.Data.Entities;
using CaptchaDemo.Core.Data.Enum;
using CaptchaDemo.Core.Data.Repositories;
using CaptchaDemo.Core.IoC.Resolver;
using CaptchaDemo.Core.Services;
using CaptchaDemo.Core.Services.Impls;

namespace CaptchaDemo.Core.IoC
{
	public class AutofacConfig
	{
		public static void ConfigureContainer(Type application)
		{
			var builder = new ContainerBuilder();

			builder.RegisterControllers(application.Assembly);

			RegisterTypes(builder);
			
			var container = builder.Build();

			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}

		private static void RegisterTypes(ContainerBuilder builder)
		{
			builder.RegisterType<GameWordsService>().Keyed<ICapthcaService>(CaptchaTypes.GameWords);
			builder.RegisterType<CaptchaMathService>().Keyed<ICapthcaService>(CaptchaTypes.RebusMath);
			builder.RegisterType<CaptchaPuzzleMathService>().Keyed<ICapthcaService>(CaptchaTypes.PuzzleMath);

			builder.RegisterType<CacheStorageProvider>().Keyed<ICaptchaStorageProvider>(false);
			builder.RegisterType<DatabaseStorageProvider>().Keyed<ICaptchaStorageProvider>(true);

			builder.RegisterType<CaptchaResolverFactory>().As<ICaptchaResolverFactory>();

			builder.RegisterType<DbConfiguration>().As<IDbConfiguration>();
			builder.Register(c => ConfigurationManager.GetSection(SectionNames.CapthaGameWordsSettings)).As<ICaptchaGameWordsConfiguration>();
			builder.Register(c => ConfigurationManager.GetSection(SectionNames.CapthaPuzzleMathSettings)).As<ICaptchaPuzzleMathConfiguration>();
			builder.Register(c => ConfigurationManager.GetSection(SectionNames.CapthaSettings)).As<ICaptchaConfiguration>();

			builder.RegisterType<StorageKeyProvider>().As<IStorageKeyProvider>();
			builder.RegisterType<ImageService>().As<IImageService>();
			builder.RegisterType<FileService>().As<IFileService>();
			builder.RegisterType<Repository<Question>>().As<IRepository<Question>>()
				.WithParameter(new TypedParameter(typeof(string), DbConsts.QuestionCollectionName));

			builder.RegisterType<RandomProvider>().As<IRandomProvider>();
			builder.RegisterType<CacheProvider>().As<ICacheProvider>();
		}
	}
}
