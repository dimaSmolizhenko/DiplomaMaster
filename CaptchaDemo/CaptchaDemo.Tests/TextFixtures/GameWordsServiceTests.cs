﻿using CaptchaDemo.Core.Services;
using CaptchaDemo.Core.Services.Impls;
using CaptchaDemo.Data.Entities;
using CaptchaDemo.Data.Enum;
using Moq;
using NUnit.Framework;

namespace CaptchaDemo.Tests.TextFixtures
{
	/// <summary>
	/// Summary description for GameWordsServiceTests
	/// </summary>
	[TestFixture]
	public class GameWordsServiceTests
	{
		private const string Id = "123456";


		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void ValidateCaptcha_AnswerOneWord_IsValid()
		{
			//arrange
			var answers = new[] {"answer"};

			var question = new Question
			{
				Id = Id,
				Answers = answers,
				ImageUrl = "url",
				Text = "question",
				Type = CaptchaTypes.GameWordsCahed.ToString()
			};

			var mockCache = new Mock<ICacheProvider>();
			mockCache.Setup(x => x.Get(Id)).Returns(question);

			var mockStorageKey = new Mock<IStorageKeyProvider>();
			var mockRandom = new Mock<IRandomProvider>();
			var mockFileService = new Mock<IFileService>();
			var mockImageService = new Mock<IImageService>();

			var captchaService = new GameWordsCachedService(mockCache.Object, mockStorageKey.Object, mockRandom.Object, mockImageService.Object, mockFileService.Object);

			//act
			var isValid = captchaService.ValidateCaptchaAsync(Id, answers);

			//assert
			Assert.IsTrue(isValid);
		}
	}
}
