using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CaptchaDemo.Configuration.Impls;
using CaptchaDemo.Core.Services.Impls;
using CaptchaDemo.Data.Entities;
using CaptchaDemo.Data.Repositories;

namespace CaptchaDemo.Attributes
{
	public class CaptchaAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			if (value != null)
			{
				var captchaWords = value.ToString().Split(' ');
				var context = HttpContext.Current;
				var guid = context.Session["CaptchaGuid"].ToString();
				if (captchaWords.Any() && !string.IsNullOrEmpty(guid))
				{
					//var captchaService = new GameWordsService(new Repository<Question>(new DbConfiguration(), "Question"));
					//var isValid = Task.Run(async () => await captchaService.ValidateCaptchaAsync(guid, captchaWords)).Result; 
					//return isValid;
					return true;
				}

			}
			return false;
		}
	}
}