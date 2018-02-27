using System.Configuration;
using CaptchaDemo.Configuration.Consts;

namespace CaptchaDemo.Configuration.Impls
{
	public class CaptchaConfiguration : ConfigurationSection, ICaptchaConfiguration
	{
		[ConfigurationProperty(ConfigurationConsts.CaptchaStoragePath, IsRequired = true)]
		public string StoragePath => base[ConfigurationConsts.CaptchaStoragePath].ToString();

		[ConfigurationProperty(ConfigurationConsts.CaptchaFilePath, IsRequired = true)]
		public string FilePathToPDF => base[ConfigurationConsts.CaptchaFilePath].ToString();
	}
}
