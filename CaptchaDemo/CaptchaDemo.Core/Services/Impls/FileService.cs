using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CaptchaDemo.Configuration;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace CaptchaDemo.Core.Services.Impls
{
	public class FileService : IFileService
	{
		#region Dependencies

		private readonly IStorageKeyProvider _storageKeyProvider;
		private readonly IRandomProvider _randomProvider;
		private readonly ICaptchaGameWordsConfiguration _captchaConfiguration;

		#endregion

		#region .ctor

		public FileService(IStorageKeyProvider storageKeyProvider, IRandomProvider randomProvider, ICaptchaGameWordsConfiguration captchaConfiguration)
		{
			_storageKeyProvider = storageKeyProvider;
			_randomProvider = randomProvider;
			_captchaConfiguration = captchaConfiguration;
		}

		#endregion

		#region Public Methods

		public IList<string> GetWordsFromFile()
		{
			var filePath = _storageKeyProvider.GetPDFFilePath();

			var wordsList = GetWords(filePath).Where(x => !string.IsNullOrEmpty(x) && x.Length > 3).ToArray();

			var list = new List<string>();

			var count = _captchaConfiguration.CaptchaWordsCount;

			for (var i = 0; i < count; i++)
			{
				var word = GetWord(wordsList, list);
				list.Add(word);
			}

			return list;
		}

		#endregion

		#region Private Methods

		private string GetWord(IReadOnlyList<string> words, List<string> captchaList)
		{
			var word = "";
			while (word.Length < 4)
			{
				var number = _randomProvider.GetRandom(words.Count);
				var randomWord = words[number];
				if (!captchaList.Contains(randomWord))
				{
					var randomWords = SplitCamelCase(words[number]);
					if (randomWords.Length == 1)
					{
						word = words[number];
					}
				}
			}

			return word;
		}

		private string[] GetWords(string filePath)
		{
			var reader = new PdfReader(filePath); //TODO: add dependency injection and change test

			var pageNumber = _randomProvider.GetRandom(reader.NumberOfPages);

			var pageText = PdfTextExtractor.GetTextFromPage(reader, pageNumber);

			var separators = new[]
			{
				' ', '.', ',', '!', '?', '(', ')', '\n', '\r', '\t', ';', '{', '}', '[', ']', ':', '=', '+', '*', '/', '>', '<',
				'%', '$', '@', '"', '\'', '_', '-', '`', '~' , '|', '\\', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
			};

			return pageText.Split(separators);
		}

		public string[] SplitCamelCase(string source)
		{
			return Regex.Split(source, @"(?<!^)(?=[A-Z])");
		}

		public string[] SplitCamelCaseAndNumbers(string source)
		{
			return Regex.Split(source, @"(\d+|[A-Za-z]+)");
		}

		#endregion
	}
}
