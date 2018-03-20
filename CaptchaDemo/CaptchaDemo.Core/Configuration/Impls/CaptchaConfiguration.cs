using System.Configuration;
using CaptchaDemo.Core.Configuration.Consts;

namespace CaptchaDemo.Core.Configuration.Impls
{
	public class CaptchaConfiguration : ConfigurationSection, ICaptchaConfiguration
	{
		[ConfigurationProperty(ConfigurationConsts.CaptchaFileStoragePath, IsRequired = true)]
		public string FileStoragePath => base[ConfigurationConsts.CaptchaFileStoragePath].ToString();

		[ConfigurationProperty(ConfigurationConsts.CaptchaWebStoragePath, IsRequired = true)]
		public string WebStoragePath => base[ConfigurationConsts.CaptchaWebStoragePath].ToString();

		[ConfigurationProperty(ConfigurationConsts.CaptchaLifeTime, IsRequired = true)]
		public int CaptchaLifeTime => (int)base[ConfigurationConsts.CaptchaLifeTime];

		[ConfigurationProperty(ConfigurationConsts.CaptchaIsUseDatabase, IsRequired = true)]
		public bool CaptchaIsUseDatabase => (bool)base[ConfigurationConsts.CaptchaIsUseDatabase];
	}
}
