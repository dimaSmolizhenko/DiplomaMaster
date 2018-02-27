using System;
using System.Drawing.Imaging;
using System.IO;
using CaptchaDemo.Configuration;

namespace CaptchaDemo.Core.Services.Impls
{
	public class StorageKeyProvider : IStorageKeyProvider
	{
		#region Dependencies

		private readonly ICaptchaConfiguration _captchaConfiguration;

		#endregion

		#region .ctor

		public StorageKeyProvider(ICaptchaConfiguration captchaConfiguration)
		{
			_captchaConfiguration = captchaConfiguration;
		}

		#endregion

		#region Public Methods

		public string GetFilePath(string fileName, string captchaType, bool needChangeFileName = false)
		{
			var fileNameResult = fileName;
			if (needChangeFileName)
			{
				var extension = Path.GetExtension(fileName);
				fileNameResult = string.Concat(Guid.NewGuid().ToString(), extension);
			}

			var path = GetCaptchaFSPath(captchaType);
			return string.Concat(path, fileNameResult);
		}

		public string GetFilePath(string captchaType, ImageFormat format)
		{
			var path = GetCaptchaFSPath(captchaType);
			var fileName = $"{Guid.NewGuid()}.{format}";
			return string.Concat(path, fileName);
		}

		public string GetFileName(string filePath)
		{
			return Path.GetFileName(filePath);
		}

		public string GetPDFFilePath()
		{
			return _captchaConfiguration.FilePathToPDF;
		}

		#endregion

		#region Private Methods

		private string GetCaptchaFSPath(string capthcaType)
		{
			var directoryPath = $"{_captchaConfiguration.StoragePath}\\{capthcaType}\\";
			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
			}

			return directoryPath;
		}

		#endregion
	}
}
