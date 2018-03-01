using System.Web;

namespace CaptchaDemo.Models
{
	public class QuestionViewModel
	{
		public string Text { get; set; }

		public HttpPostedFileBase File { get; set; }

		public string Answer { get; set; }

		public string Type { get; set; }
	}
}