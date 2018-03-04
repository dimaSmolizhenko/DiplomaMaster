namespace CaptchaDemo.Core.Services
{
	public interface IRandomProvider
	{
		int GetRandom(int min, int max);
		int GetRandom(int max);
	}
}
