using System.Drawing;
using CaptchaDemo.Data.Enum;

namespace CaptchaDemo.Core.Models
{
	public class CapthcaConfig
	{
		public CapthcaConfig()
		{
			Height = 80;
			Width = 250;
			BackgroundColor = Color.LightSlateGray;
			FontColor = Color.Black;
			FontFamily = "Comic Sans";
			CaptchaType = CaptchaTypes.GameWords.ToString();
		}

		public string FontFamily { get; set; }

		public Color FontColor { get; set; }

		public Color BackgroundColor { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }

		public string CaptchaType { get; set; }
	}
}
