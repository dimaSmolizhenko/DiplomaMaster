using System.Configuration;
using CaptchaDemo.Core.Configuration.Consts;

namespace CaptchaDemo.Core.Configuration.Impls
{
	public class CaptchaPuzzleMathConfiguration : ConfigurationSection, ICaptchaPuzzleMathConfiguration
	{
		[ConfigurationProperty(ConfigurationConsts.GenerateFromImagePath, IsRequired = true)]
		public string GenerateFromImagePath => base[ConfigurationConsts.GenerateFromImagePath].ToString();
	}
}
