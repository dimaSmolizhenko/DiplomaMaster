namespace CaptchaDemo.Configuration
{
	public interface ICaptchaConfiguration
	{
		string FileStoragePath { get; }
		string FilePathToPDF { get; }
		string WebStoragePath { get; }
		int CaptchaLifeTime { get; }
		int CaptchaWordsCount { get; }
	}
}
