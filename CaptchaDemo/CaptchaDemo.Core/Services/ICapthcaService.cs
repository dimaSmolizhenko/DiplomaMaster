using System.Threading.Tasks;
using CaptchaDemo.Data.Entities;

namespace CaptchaDemo.Core.Services
{
	public interface ICapthcaService
	{
		Task<bool> ValidateCaptchaAsync(string guid, string[] answers);
		Task<Question> GetCapthaAsync();
	}
}
