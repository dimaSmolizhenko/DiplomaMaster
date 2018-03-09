using System.Threading.Tasks;
using CaptchaDemo.Data.BussinessModels;

namespace CaptchaDemo.Core.Services
{
	public interface ICapthcaService
	{
		bool ValidateCaptchaAsync(string guid, string[] answers);
		QuestionModel GetCapthaAsync();
	}
}
