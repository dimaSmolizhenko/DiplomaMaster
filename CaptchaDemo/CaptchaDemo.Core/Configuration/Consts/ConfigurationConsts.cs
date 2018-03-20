namespace CaptchaDemo.Core.Configuration.Consts
{
	internal static class ConfigurationConsts
	{
		public static string MongoDbConfigurationName => "MongoDb";

		public const string CaptchaFileStoragePath = "fileStoragePath";

		public const string CaptchaFilePath = "filePathToPDF";

		public const string CaptchaWebStoragePath = "webStoragePath";

		public const string CaptchaLifeTime = "captchaLifeTimeMinutes";

		public const string CaptchaWordsCount = "captchaWordsCount";

		public const string GenerateFromImagePath = "generateFromImagePath";

		public const string CaptchaIsUseDatabase = "useDatabase";
	}
}
