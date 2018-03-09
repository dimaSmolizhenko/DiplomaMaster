using System.Threading.Tasks;
using CaptchaDemo.Data.BussinessModels;
using CaptchaDemo.Data.Entities;
using CaptchaDemo.Data.Enum;
using CaptchaDemo.Data.Repositories;

namespace CaptchaDemo.Core.Services.Impls
{
	public class CaptchaMathService : BaseCaptchaService, ICapthcaService
	{
		private readonly IRepository<Question> _repository;
		private readonly IRandomProvider _randomProvider;

		public CaptchaMathService(IRepository<Question> repository, IStorageKeyProvider storageKeyProvider, 
			IRandomProvider randomProvider) : base(storageKeyProvider)
		{
			_repository = repository;
			_randomProvider = randomProvider;
		}

		public bool ValidateCaptchaAsync(string guid, string[] answers)
		{
			var question = Task.Run(async () => await _repository.GetByIdAsync(guid)).Result;

			return Contains(question.Answers, answers);
		}

		public QuestionModel GetCapthaAsync()
		{
			var questions = Task.Run(async () => await _repository.GetByTypeAsync(CaptchaTypes.RebusMath.ToString())).Result;
			var randomNumber = _randomProvider.GetRandom(questions.Count);
			var question = questions[randomNumber];

			return MapQuestionToQuestionModel(question);
		}
	}
}
