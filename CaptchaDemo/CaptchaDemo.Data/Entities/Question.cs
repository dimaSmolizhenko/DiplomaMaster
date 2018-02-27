using CaptchaDemo.Data.CommonMemebers.Impls;

namespace CaptchaDemo.Data.Entities
{
	public class Question : Entity
	{
		public string Text { get; set; }

		public string ImageUrl { get; set; }

		public string[] Answers { get; set; }

		public string Type { get; set; }

	}
}
