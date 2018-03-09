using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CaptchaDemo.Data.BussinessModels;
using CaptchaDemo.Data.Entities;
using CaptchaDemo.Data.Enum;

namespace CaptchaDemo.Core.Services.Impls
{
	public class GameWordsCachedService : BaseCaptchaService, ICapthcaService
	{
		#region Dependencies

		private readonly ICacheProvider _cacheProvider;
		private readonly IFileService _fileService;
		private readonly IImageService _imageService;
		private readonly IRandomProvider _randomProvider;

		#endregion

		#region .ctor

		public GameWordsCachedService(ICacheProvider cacheProvider, IStorageKeyProvider storageKeyProvider, 
			IRandomProvider randomProvider, IImageService imageService, 
			IFileService fileService) : base(storageKeyProvider)
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
			var question = _cacheProvider.Get(guid) as Question;

			var isCorrect = question != null && Contains(question.Answers, answers);
			if (isCorrect)
			{
				_cacheProvider.Delete(guid);
			}

			return isCorrect;
		}

		public QuestionModel GetCapthaAsync()
		{
			var words = _fileService.GetWordsFromFile();
			var questionText = JoinRandomChars(words);
			var imageUrl = _imageService.CreateImage(questionText);
			var question = CreateQuestion(words, imageUrl, questionText);

			_cacheProvider.Add(question.Id, question);

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
				Id = Guid.NewGuid().ToString(),
				ImageUrl = imageUrl,
				Answers = words.ToArray(),
				Text = questionText,
				Type = CaptchaTypes.GameWords.ToString()
			};
		}

		#endregion
	}
}
