namespace CaptchaDemo.Configuration
{
	public interface ICaptchaConfiguration
	{
		string FileStoragePath { get; }
		string WebStoragePath { get; }
		int CaptchaLifeTime { get; }
		bool CaptchaIsUseDatabase { get; }
	}
}
