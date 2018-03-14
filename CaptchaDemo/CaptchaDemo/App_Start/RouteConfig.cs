using System.Web.Mvc;
using System.Web.Routing;

namespace CaptchaDemo
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "HomeDemo", action = "Index", id = UrlParameter.Optional },
				namespaces: new string[] { "CaptchaDemo.Web.Controllers" }
			);
		}
	}
}
