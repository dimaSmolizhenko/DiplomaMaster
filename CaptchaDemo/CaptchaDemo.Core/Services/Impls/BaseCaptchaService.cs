using System.Collections.Generic;
using System.Linq;
using CaptchaDemo.Data.BussinessModels;
using CaptchaDemo.Data.Entities;

namespace CaptchaDemo.Core.Services.Impls
{
	public class BaseCaptchaService
	{
		private readonly IStorageKeyProvider _storageKeyProvider;

		public BaseCaptchaService(IStorageKeyProvider storageKeyProvider)
		{
			_storageKeyProvider = storageKeyProvider;
		}

		protected virtual QuestionModel MapQuestionToQuestionModel(Question question)
		{
			return new QuestionModel
			{
				QuestionId = question.Id,
				Text = question.Text,
				Type = question.Type,
				Answers = question.Answers.Select(x => x).ToArray(),
				ImageUrl = _storageKeyProvider.GetWebFilePath(question.Type, question.ImageUrl)
			};
		}

		protected virtual bool Contains(IReadOnlyCollection<string> dbAnswers, IReadOnlyCollection<string> answers)
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
	}
}
