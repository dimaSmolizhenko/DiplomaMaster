using CaptchaDemo.Core.Data.BussinessModels;

namespace CaptchaDemo.Core.Services
{
	public interface ICapthcaService
	{
		bool ValidateCaptcha(string guid, string answer);
		QuestionModel GetCaptha();
	}
}
