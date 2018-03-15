namespace CaptchaDemo.Configuration
{
	public interface ICaptchaGameWordsConfiguration
	{
		string FilePathToPDF { get; }
		int CaptchaWordsCount { get; }
	}
}
