﻿using System.Collections.Generic;
using System.Linq;
using CaptchaDemo.Configuration;
using CaptchaDemo.Core.IoC.Resolver;
using CaptchaDemo.Data.BussinessModels;
using CaptchaDemo.Data.Entities;

namespace CaptchaDemo.Core.Services.Impls
{
	public abstract class BaseCaptchaService
	{
		protected readonly IStorageKeyProvider StorageKeyProvider;
		protected readonly ICaptchaStorageProvider CaptchaStorageProvider;

		protected BaseCaptchaService(IStorageKeyProvider storageKeyProvider, 
			ICaptchaConfiguration captchaConfiguration, ICaptchaResolverFactory captchaResolverFactory)
		{
			StorageKeyProvider = storageKeyProvider;
			CaptchaStorageProvider = captchaResolverFactory.GetStorageProvider(captchaConfiguration.CaptchaIsUseDatabase);

		}

		protected virtual QuestionModel MapQuestionToQuestionModel(Question question)
		{
			return new QuestionModel
			{
				QuestionId = question.Id,
				Text = question.Text,
				Type = question.Type,
				Answers = question.Answers.Select(x => x).ToArray(),
				ImageUrl = StorageKeyProvider.GetWebFilePath(question.Type, question.ImageUrl)
			};
		}

		protected virtual bool ContainsAll(IReadOnlyCollection<string> dbAnswers, IReadOnlyCollection<string> answers)
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

		protected virtual bool ContainsAny(IReadOnlyCollection<string> dbAnswers, IReadOnlyCollection<string> answers)
		{
			return answers.Any(dbAnswers.Contains);
		}
	}
}
