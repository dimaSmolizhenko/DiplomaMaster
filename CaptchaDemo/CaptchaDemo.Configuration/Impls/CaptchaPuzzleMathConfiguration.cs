using System.Configuration;
using CaptchaDemo.Configuration.Consts;

namespace CaptchaDemo.Configuration.Impls
{
	public class CaptchaPuzzleMathConfiguration : ConfigurationSection, ICaptchaPuzzleMathConfiguration
	{
		[ConfigurationProperty(ConfigurationConsts.GenerateFromImagePath, IsRequired = true)]
		public string GenerateFromImagePath => base[ConfigurationConsts.GenerateFromImagePath].ToString();
	}
}
