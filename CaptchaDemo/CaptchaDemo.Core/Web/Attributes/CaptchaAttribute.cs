using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CaptchaDemo.Core.Data.Enum;
using CaptchaDemo.Core.IoC.Resolver;

namespace CaptchaDemo.Core.Web.Attributes
{
	public class CaptchaAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			if (value != null)
			{
				var captchaAnswer = value.ToString();
				var context = HttpContext.Current; //TODO: to sessionProvider
				var guid = context.Session["CaptchaGuid"].ToString();
				var type = (CaptchaTypes)context.Session["CaptchaType"];

				if (!string.IsNullOrEmpty(captchaAnswer) && !string.IsNullOrEmpty(guid))
				{
					var captchaResolverFactory = new CaptchaResolverFactory();
					var captchaService = captchaResolverFactory.GetServiceByType(type);
					var isValid = captchaService.ValidateCaptcha(guid, captchaAnswer); 
					return isValid;
				}
			}
			return false;
		}
	}
}