namespace CaptchaDemo.Configuration
{
	public interface ICaptchaConfiguration
	{
		string StoragePath { get; }
		string FilePathToPDF { get; }
	}
}
