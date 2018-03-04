using System;
using System.Security.Cryptography;

namespace CaptchaDemo.Core.Services.Impls
{
	public class RandomProvider : IRandomProvider
	{
		#region Public Methods

		public int GetRandom(int min, int max)
		{
			var seed = GetRandomCrypto();
			var random = new Random(seed);
			return random.Next(min, max);
		}

		public int GetRandom(int max)
		{
			return GetRandom(0, max);
		}

		#endregion

		#region Private Methods

		private int GetRandomCrypto()
		{
			var generator = RandomNumberGenerator.Create();
			byte[] rndArray = new byte[4];
			generator.GetBytes(rndArray);
			return BitConverter.ToInt32(rndArray, 0);
		}

		#endregion
	}
}
