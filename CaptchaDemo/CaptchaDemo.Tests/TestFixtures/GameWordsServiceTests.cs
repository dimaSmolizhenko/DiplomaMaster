using CaptchaDemo.Core.Configuration;
using CaptchaDemo.Core.Data.Entities;
using CaptchaDemo.Core.Data.Enum;
using CaptchaDemo.Core.IoC.Resolver;
using CaptchaDemo.Core.Services;
using CaptchaDemo.Core.Services.Impls;
using Moq;
using NUnit.Framework;

namespace CaptchaDemo.Tests.TestFixtures
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
				Type = CaptchaTypes.GameWords.ToString()
			};

			var mockCache = new Mock<ICacheProvider>();
			mockCache.Setup(x => x.Get(Id)).Returns(question);

			var mockStorageKey = new Mock<IStorageKeyProvider>();
			var mockRandom = new Mock<IRandomProvider>();
			var mockFileService = new Mock<IFileService>();
			var mockImageService = new Mock<IImageService>();
			var mockConfiguration = new Mock<ICaptchaConfiguration>();
			var mockResolver = new Mock<ICaptchaResolverFactory>();

			var captchaService = new GameWordsService(mockCache.Object, mockStorageKey.Object, 
				mockRandom.Object, mockImageService.Object, mockFileService.Object, 
				mockConfiguration.Object, mockResolver.Object);

			//act
			var isValid = captchaService.ValidateCaptcha(Id, answers);

			//assert
			Assert.IsTrue(isValid);
		}
	}
}
