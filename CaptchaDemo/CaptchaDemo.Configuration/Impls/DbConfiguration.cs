using System.Configuration;
using CaptchaDemo.Configuration.Consts;

namespace CaptchaDemo.Configuration.Impls
{
	public class DbConfiguration : IDbConfiguration
	{
		public string GetConnectionString()
		{
			return ConfigurationManager.ConnectionStrings[ConfigurationConsts.MongoDbConfigurationName].ConnectionString;
		}
	}
}
