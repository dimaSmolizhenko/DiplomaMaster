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
			var fileName = $"{Guid.NewGuid()}.{format.ToString().ToLowerInvariant()}";
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

		public string GetWebFilePath(string captchaType, string fileName)
		{
			return CombineWebPath(captchaType, fileName);
		}

		#endregion

		#region Private Methods

		private string GetCaptchaFSPath(string captchaType)
		{
			var directoryPath = GetCaptchaFSDirectory(captchaType);
			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
			}

			return directoryPath;
		}

		private string GetCaptchaFSDirectory(string captchaType)
		{
			return FormatFSLink(
				$"{_captchaConfiguration.FileStoragePath}{_captchaConfiguration.WebStoragePath}\\{captchaType}\\");
		}

		private string CombineWebPath(params string[] pathes)
		{
			return FormatWebLink($"{Path.Combine(_captchaConfiguration.WebStoragePath, Path.Combine(pathes))}");
		}

		private string FormatWebLink(string path)
		{
			if (!string.IsNullOrEmpty(path))
			{
				return path.Replace(@"\", "/");
			}
			return path;
		}

		private string FormatFSLink(string path)
		{
			if (!string.IsNullOrEmpty(path))
			{
				return path.Replace("/", @"\");
			}
			return path;
		}

		#endregion
	}
}
