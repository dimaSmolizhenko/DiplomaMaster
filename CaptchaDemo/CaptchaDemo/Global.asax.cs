﻿using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CaptchaDemo.IoC;

namespace CaptchaDemo
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{

			AutofacConfig.ConfigureContainer(typeof(MvcApplication));

			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}
}
