using System.Configuration;
using CaptchaDemo.Core.Configuration.Consts;

namespace CaptchaDemo.Core.Configuration.Impls
{
	public class DbConfiguration : IDbConfiguration
	{
		public string GetConnectionString()
		{
			return ConfigurationManager.ConnectionStrings[ConfigurationConsts.MongoDbConfigurationName].ConnectionString;
		}
	}
}
