using System.Drawing.Imaging;

namespace CaptchaDemo.Core.Services
{
	public interface IStorageKeyProvider
	{
		string GetFilePath(string fileName, string captchaType, bool needChangeFileName = false);
		string GetFilePath(string captchaType, ImageFormat format);
		string GetFileName(string filePath);
		string GetPDFFilePath();
		string GetWebFilePath(string captchaType, string fileName);
		string[] GetFilesFromDirectory(string path);
	}
}
