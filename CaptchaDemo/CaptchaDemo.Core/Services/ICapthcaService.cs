using System.Threading.Tasks;
using CaptchaDemo.Data.BussinessModels;

namespace CaptchaDemo.Core.Services
{
	public interface ICapthcaService
	{
		Task<bool> ValidateCaptchaAsync(string guid, string[] answers);
		Task<QuestionModel> GetCapthaAsync();
	}
}
