using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaptchaDemo.Core.Models;
using CaptchaDemo.Data.Entities;
using CaptchaDemo.Data.Enum;
using CaptchaDemo.Data.Repositories;

namespace CaptchaDemo.Core.Services.Impls
{
	public class GameWordsService : ICapthcaService
	{
		#region Dependencies

		private readonly IRepository<Question> _repository;
		private readonly IImageService _imageService;
		private readonly IFileService _fileService;

		#endregion

		#region .ctor

		public GameWordsService(IRepository<Question> repository, IImageService imageService, IFileService fileService)
		{
			_repository = repository;
			_imageService = imageService;
			_fileService = fileService;
		}

		#endregion

		#region Public Methods

		public async Task<bool> ValidateCaptchaAsync(string guid, string[] answers)
		{
			var question = await _repository.GetByIdAsync(guid);

			return Contains(question.Answers, answers);
		}

		public async Task<Question> GetCapthaAsync()
		{
			var words = _fileService.GetWordsFromFile();
			var questionText = JoinRandomChars(words);
			var imageUrl = _imageService.CreateImage(questionText);
			var question = CreateQuestion(words, imageUrl, questionText);

			await _repository.InsertAsync(question);

			return question;
		}

		#endregion

		#region Private Methods

		private bool Contains(IReadOnlyCollection<string> dbAnswers, IReadOnlyCollection<string> answers)
		{
			var contains = false;

			if (dbAnswers.Count != answers.Count) { return false; }

			foreach (var answer in answers)
			{
				if (dbAnswers.Contains(answer))
				{
					contains = true;
				}
				else
				{
					return false;
				}
			}

			return contains;
		}

		private string RandomChars(int length)
		{
			var random = new Random();
			const string chars = "abcdefghijklmnopqrstuvwxyz";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[random.Next(s.Length)])
				.ToArray());
		}

		private string JoinRandomChars(IEnumerable<string> words)
		{
			var stringBuilder = new StringBuilder();

			foreach (var t in words)
			{
				var random = new Random();
				var strLength = random.Next(3, 5);
				var randomChars = RandomChars(strLength);
				stringBuilder.Append(randomChars);
				stringBuilder.Append(t);
			}

			stringBuilder.Append(RandomChars(3));

			return stringBuilder.ToString();
		}

		private Question CreateQuestion(IEnumerable<string> words, string imageUrl, string questionText)
		{
			return new Question
			{
				ImageUrl = imageUrl,
				Answers = words.ToArray(),
				Text = questionText,
				Type = CaptchaTypes.GameWords.ToString()
			};
		}

		#endregion
	}
}
