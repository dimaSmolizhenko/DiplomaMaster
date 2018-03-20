namespace CaptchaDemo.Core.Data.BussinessModels
{
	public class QuestionModel
	{
		public string QuestionId { get; set; }
		public string Text { get; set; }

		public string ImageUrl { get; set; }

		public string[] Answers { get; set; }

		public string Type { get; set; }
	}
}
