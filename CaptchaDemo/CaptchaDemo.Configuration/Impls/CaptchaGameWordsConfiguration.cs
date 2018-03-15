using System.Configuration;
using CaptchaDemo.Configuration.Consts;

namespace CaptchaDemo.Configuration.Impls
{
	public class CaptchaGameWordsConfiguration : ConfigurationSection, ICaptchaGameWordsConfiguration
	{
		[ConfigurationProperty(ConfigurationConsts.CaptchaFilePath, IsRequired = true)]
		public string FilePathToPDF => base[ConfigurationConsts.CaptchaFilePath].ToString();

		[ConfigurationProperty(ConfigurationConsts.CaptchaWordsCount, IsRequired = true)]
		public int CaptchaWordsCount => (int) base[ConfigurationConsts.CaptchaWordsCount];
	}
}
