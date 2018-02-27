using System;
using System.Collections.Generic;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace CaptchaDemo.Core.Services.Impls
{
	public class FileService : IFileService
	{
		#region Dependencies

		private readonly IStorageKeyProvider _storageKeyProvider;

		#endregion

		#region .ctor

		public FileService(IStorageKeyProvider storageKeyProvider)
		{
			_storageKeyProvider = storageKeyProvider;
		}

		#endregion

		#region Public Methods

		public IList<string> GetWordsFromFile(int count = 1)
		{
			var filePath = _storageKeyProvider.GetPDFFilePath();

			var wordsList = GetWords(filePath);

			var list = new List<string>();

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
			var random = new Random();
			var word = "";
			while (word.Length < 4)
			{
				var number = random.Next(0, words.Count);
				if (!captchaList.Contains(words[number]))
				{
					word = words[number];
				}
			}

			return word;
		}

		private string[] GetWords(string filePath)
		{
			var reader = new PdfReader(filePath);

			var random = new Random();
			var pageNumber = random.Next(0, reader.NumberOfPages);

			var pageText = PdfTextExtractor.GetTextFromPage(reader, pageNumber);
			var separators = new[] { ' ', '.', ',', '!', '?', '(', ')', '\n', '\r', '\t', ';', '{', '}', '[', ']', ':' };

			return pageText.Split(separators);
		}

		#endregion
	}
}
