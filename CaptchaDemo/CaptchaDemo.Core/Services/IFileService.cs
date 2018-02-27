using System.Collections.Generic;

namespace CaptchaDemo.Core.Services
{
	public interface IFileService
	{
		IList<string> GetWordsFromFile(int count = 1);
	}
}
