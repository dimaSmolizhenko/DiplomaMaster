using System;
using System.Configuration;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using CaptchaDemo.Configuration;
using CaptchaDemo.Configuration.Consts;
using CaptchaDemo.Configuration.Impls;
using CaptchaDemo.Core.Services;
using CaptchaDemo.Core.Services.Impls;
using CaptchaDemo.Data.Consts;
using CaptchaDemo.Data.Entities;
using CaptchaDemo.Data.Enum;
using CaptchaDemo.Data.Repositories;
using CaptchaDemo.IoC.Resolver;

namespace CaptchaDemo.IoC
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

			builder.RegisterType<CaptchaResolverFactory>().As<ICaptchaResolverFactory>();

			builder.RegisterType<DbConfiguration>().As<IDbConfiguration>();
			builder.RegisterType<StorageKeyProvider>().As<IStorageKeyProvider>();
			builder.RegisterType<ImageService>().As<IImageService>();
			builder.RegisterType<FileService>().As<IFileService>();
			builder.Register(c => ConfigurationManager.GetSection(SectionNames.CapthaSettings)).As<ICaptchaConfiguration>();
			builder.RegisterType<Repository<Question>>().As<IRepository<Question>>()
				.WithParameter(new TypedParameter(typeof(string), DbConsts.QuestionCollectionName));
		}
	}
}
