using System.Configuration;
using CaptchaDemo.Configuration.Consts;

namespace CaptchaDemo.Configuration.Impls
{
	public class CaptchaConfiguration : ConfigurationSection, ICaptchaConfiguration
	{
		[ConfigurationProperty(ConfigurationConsts.CaptchaFileStoragePath, IsRequired = true)]
		public string FileStoragePath => base[ConfigurationConsts.CaptchaFileStoragePath].ToString();

		[ConfigurationProperty(ConfigurationConsts.CaptchaFilePath, IsRequired = true)]
		public string FilePathToPDF => base[ConfigurationConsts.CaptchaFilePath].ToString();

		[ConfigurationProperty(ConfigurationConsts.CaptchaWebStoragePath, IsRequired = true)]
		public string WebStoragePath => base[ConfigurationConsts.CaptchaWebStoragePath].ToString();

		[ConfigurationProperty(ConfigurationConsts.CaptchaLifeTime, IsRequired = true)]
		public int CaptchaLifeTime => (int)base[ConfigurationConsts.CaptchaLifeTime];
	}
}
