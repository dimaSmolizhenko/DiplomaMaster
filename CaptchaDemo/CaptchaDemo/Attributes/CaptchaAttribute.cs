using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CaptchaDemo.Configuration.Impls;
using CaptchaDemo.Core.Services.Impls;
using CaptchaDemo.Data.Entities;
using CaptchaDemo.Data.Enum;
using CaptchaDemo.Data.Repositories;
using CaptchaDemo.IoC.Resolver;

namespace CaptchaDemo.Attributes
{
	public class CaptchaAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			if (value != null)
			{
				var captchaWords = value.ToString().Split(' ');
				var context = HttpContext.Current; //TODO: to sessionProvider
				var guid = context.Session["CaptchaGuid"].ToString();
				var type = (CaptchaTypes)context.Session["CaptchaType"];

				if (captchaWords.Any() && !string.IsNullOrEmpty(guid))
				{
					var captchaResolverFactory = new CaptchaResolverFactory();
					var captchaService = captchaResolverFactory.GetServiceByType(type);
					var isValid = Task.Run(async () => await captchaService.ValidateCaptchaAsync(guid, captchaWords)).Result; 
					return isValid;
				}

			}
			return false;
		}
	}
}