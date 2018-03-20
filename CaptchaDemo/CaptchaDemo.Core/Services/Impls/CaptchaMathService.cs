using System.Threading.Tasks;
using CaptchaDemo.Core.Configuration;
using CaptchaDemo.Core.Data.BussinessModels;
using CaptchaDemo.Core.Data.Entities;
using CaptchaDemo.Core.Data.Enum;
using CaptchaDemo.Core.Data.Repositories;
using CaptchaDemo.Core.IoC.Resolver;

namespace CaptchaDemo.Core.Services.Impls
{
	public class CaptchaMathService : BaseCaptchaService, ICapthcaService
	{
		private readonly IRepository<Question> _repository;
		private readonly IRandomProvider _randomProvider;

		public CaptchaMathService(IRepository<Question> repository, IStorageKeyProvider storageKeyProvider, 
			IRandomProvider randomProvider, ICaptchaConfiguration captchaConfiguration, ICaptchaResolverFactory captchaResolverFactory) : base(storageKeyProvider, captchaConfiguration, captchaResolverFactory)
		{
			_repository = repository;
			_randomProvider = randomProvider;
		}

		public bool ValidateCaptcha(string guid, string[] answers)
		{
			var question = Task.Run(async () => await _repository.GetByIdAsync(guid)).Result;

			return ContainsAll(question.Answers, answers);
		}

		public QuestionModel GetCaptha()
		{
			var questions = Task.Run(async () => await _repository.GetByTypeAsync(CaptchaTypes.RebusMath.ToString())).Result;
			var randomNumber = _randomProvider.GetRandom(questions.Count);
			var question = questions[randomNumber];

			return MapQuestionToQuestionModel(question);
		}
	}
}
