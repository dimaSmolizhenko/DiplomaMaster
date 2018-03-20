using CaptchaDemo.Core.Web.Attributes;

namespace CaptchaDemo.Models
{
	public class ContactViewMode
	{
		public string Title { get; set; }

		public string Author { get; set; }

		public int PageCount { get; set; }

		[Captcha]
		public string Captcha { get; set; }

	}
}