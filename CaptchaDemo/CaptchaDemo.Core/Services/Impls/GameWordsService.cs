using System.Collections.Generic;
using System.Linq;
using System.Text;
using CaptchaDemo.Configuration;
using CaptchaDemo.Core.IoC.Resolver;
using CaptchaDemo.Data.BussinessModels;
using CaptchaDemo.Data.Entities;
using CaptchaDemo.Data.Enum;

namespace CaptchaDemo.Core.Services.Impls
{
	public class GameWordsService : BaseCaptchaService, ICapthcaService
	{
		#region Dependencies

		private readonly ICacheProvider _cacheProvider;
		private readonly IFileService _fileService;
		private readonly IImageService _imageService;
		private readonly IRandomProvider _randomProvider;

		#endregion

		#region .ctor

		public GameWordsService(ICacheProvider cacheProvider, IStorageKeyProvider storageKeyProvider, 
			IRandomProvider randomProvider, IImageService imageService, 
			IFileService fileService, ICaptchaConfiguration captchaConfiguration, 
			ICaptchaResolverFactory captchaResolverFactory) : base(storageKeyProvider, captchaConfiguration, captchaResolverFactory)
		{
			_cacheProvider = cacheProvider;
			_randomProvider = randomProvider;
			_imageService = imageService;
			_fileService = fileService;
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
			var words = _fileService.GetWordsFromFile();
			var captchaText = JoinRandomChars(words);
			var questionText = "Find words in current string.";
			var imageUrl = _imageService.CreateImage(captchaText);
			var question = CreateQuestion(words, imageUrl, questionText);

			CaptchaStorageProvider.Insert(question);

			return MapQuestionToQuestionModel(question);
		}

		#endregion

		#region Private Methods

		private string RandomChars(int length)
		{
			const string chars = "abcdefghijklmnopqrstuvwxyz";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[_randomProvider.GetRandom(s.Length)])
				.ToArray());
		}

		private string JoinRandomChars(IEnumerable<string> words)
		{
			var stringBuilder = new StringBuilder();

			foreach (var t in words)
			{
				var strLength = _randomProvider.GetRandom(3, 7);
				var randomChars = RandomChars(strLength);
				stringBuilder.Append(randomChars);
				stringBuilder.Append(t);
			}

			var randLength = _randomProvider.GetRandom(3, 7);
			stringBuilder.Append(RandomChars(randLength));

			return stringBuilder.ToString().ToLowerInvariant();
		}

		private Question CreateQuestion(IEnumerable<string> words, string imageUrl, string questionText)
		{
			return new Question
			{
				Id = CaptchaStorageProvider.CreateIdentifier(),
				ImageUrl = imageUrl,
				Answers = words.ToArray(),
				Text = questionText,
				Type = CaptchaTypes.GameWords.ToString()
			};
		}

		#endregion
	}
}
