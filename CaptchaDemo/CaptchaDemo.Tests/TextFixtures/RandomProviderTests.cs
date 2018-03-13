using CaptchaDemo.Core.Services;
using CaptchaDemo.Core.Services.Impls;
using NUnit.Framework;

namespace CaptchaDemo.Tests.TextFixtures
{
	[TestFixture]
	public class RandomProviderTests
	{
		private IRandomProvider _randomProvider;
		private const int IntervalFrom = 2;
		private const int IntervalTo = 6;

		[SetUp]
		public void Setup()
		{
			_randomProvider = new RandomProvider();
		}

		[Test]
		public void GetRandom_OneNumber_BetweenInterval()
		{
			var randomNumb = _randomProvider.GetRandom(IntervalTo);

			Assert.IsTrue(randomNumb >= 1 && randomNumb < IntervalTo);
		}

		[Test]
		public void GetRandom_SameIputData_NotRepeat()
		{
			var randomNumb1 = _randomProvider.GetRandom(IntervalTo);
			var randomNumb2 = _randomProvider.GetRandom(IntervalTo);

			Assert.AreNotSame(randomNumb1, randomNumb2);
		}

		[Test]
		public void GetRandom_TwoNumbers_BetweenInterval()
		{
			var randomNumb = _randomProvider.GetRandom(IntervalFrom, IntervalTo);

			Assert.IsTrue(randomNumb >= IntervalFrom && randomNumb < IntervalTo);
		}
	}
}
