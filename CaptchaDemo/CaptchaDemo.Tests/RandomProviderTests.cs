using CaptchaDemo.Core.Services;
using CaptchaDemo.Core.Services.Impls;
using NUnit.Framework;

namespace CaptchaDemo.Tests
{
	public class RandomProviderTests
	{
		private IRandomProvider _randomProvider;

		[SetUp]
		public void TestInit()
		{
			_randomProvider = new RandomProvider();
		}

		[Test]
		public void GetRandom_Zero_zero()
		{
			const int randomMax = 6;
			var randomNumb = _randomProvider.GetRandom(randomMax);

			Assert.AreNotEqual(randomNumb, 0);
		}

		[Test]
		public void GetRandom_SameIputData_NotRepeat()
		{
			const int randomMax = 6;
			var randomNumb1 = _randomProvider.GetRandom(randomMax);
			var randomNumb2 = _randomProvider.GetRandom(randomMax);

			Assert.AreNotSame(randomNumb1, randomNumb2);
		}

		[Test]
		public void GetRandom_TwoNumbers_BetweenInterval()
		{
			const int intervalFrom = 2;
			const int intervalTo = 6;
			var randomNumb = _randomProvider.GetRandom(intervalFrom, intervalTo);

			Assert.IsTrue(randomNumb >= intervalFrom && randomNumb < intervalTo);
		}
	}
}
