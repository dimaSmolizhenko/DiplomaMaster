using CaptchaDemo.Configuration;
using CaptchaDemo.Core.Services;
using CaptchaDemo.Core.Services.Impls;
using Moq;
using NUnit.Framework;

namespace CaptchaDemo.Tests.TextFixtures
{
	[TestFixture]
	public class FileServiceTests
	{

		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void GetWordsFromFile_CountIs2_Return2Words()
		{
			const int countOfWords = 2;
			var mock = new Mock<ICaptchaGameWordsConfiguration>();
			mock.Setup(x => x.CaptchaWordsCount).Returns(countOfWords);

			var mockStorageKey = new Mock<IStorageKeyProvider>();
			mockStorageKey.Setup(x => x.GetPDFFilePath()).Returns(@"C:\Users\Dima\Downloads\Entries.pdf");

			var mockRandom = new Mock<IRandomProvider>();
			var count = 1;
			mockRandom.Setup(x => x.GetRandom(It.IsAny<int>()))
				.Returns(() => count)
				.Callback(() => count++);

			var fileService = new FileService(mockStorageKey.Object, mockRandom.Object, mock.Object);

			var words = fileService.GetWordsFromFile();

			Assert.AreEqual(countOfWords, words.Count);
		}
	}
}
