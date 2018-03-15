using System.Collections.Generic;
using System.Linq;
using CaptchaDemo.Configuration;
using CaptchaDemo.Core.IoC.Resolver;
using CaptchaDemo.Data.BussinessModels;
using CaptchaDemo.Data.Entities;
using CaptchaDemo.Data.Enum;

namespace CaptchaDemo.Core.Services.Impls
{
	public class CaptchaPuzzleMathService : BaseCaptchaService, ICapthcaService
	{
		#region Dependencies

		private readonly IImageService _imageService;
		private readonly ICaptchaPuzzleMathConfiguration _puzzleMathConfiguration;
		private readonly IRandomProvider _randomProvider;

		#endregion

		#region .ctor

		public CaptchaPuzzleMathService(IStorageKeyProvider storageKeyProvider, IImageService imageService, 
			ICaptchaPuzzleMathConfiguration puzzleMathConfiguration, IRandomProvider randomProvider, 
			ICaptchaConfiguration captchaConfiguration, ICaptchaResolverFactory captchaResolverFactory) : base(storageKeyProvider, captchaConfiguration, captchaResolverFactory)
		{
			_imageService = imageService;
			_puzzleMathConfiguration = puzzleMathConfiguration;
			_randomProvider = randomProvider;
		}

		#endregion

		#region Public Methods

		public bool ValidateCaptchaAsync(string guid, string[] answers)
		{
			var question = CaptchaStorageProvider.Get(guid);

			var isCorrect = question != null && ContainsAll(question.Answers, answers);
			if (isCorrect)
			{
				CaptchaStorageProvider.Delete(guid);
			}

			return isCorrect;
		}

		public QuestionModel GetCapthaAsync()
		{
			var directoryPath = _puzzleMathConfiguration.GenerateFromImagePath;
			var imagePathes = StorageKeyProvider.GetFilesFromDirectory(directoryPath);
			var randomIndex = _randomProvider.GetRandom(0, imagePathes.Length);
			var resultModel = _imageService.CreateImageFromIcon(imagePathes[randomIndex]);
			var questionText = "Please input count of objects."; //TODO: move to contants
			var question = CreateQuestion(new[] { resultModel.Answer }, resultModel.ImageUrl, questionText);

			CaptchaStorageProvider.Insert(question);

			return MapQuestionToQuestionModel(question);
		}

		#endregion

		#region Private Methods

		private Question CreateQuestion(IEnumerable<string> words, string imageUrl, string questionText)
		{
			return new Question
			{
				Id = CaptchaStorageProvider.CreateIdentifier(),
				ImageUrl = imageUrl,
				Answers = words.ToArray(),
				Text = questionText,
				Type = CaptchaTypes.PuzzleMath.ToString()
			};
		}

		#endregion
	}
}
