using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using CaptchaDemo.Core.Models;

namespace CaptchaDemo.Core.Services.Impls
{
	public class ImageService : IImageService
	{
		#region Dependencies

		private readonly IStorageKeyProvider _storageKeyProvider;

		#endregion

		#region .ctor

		public ImageService(IStorageKeyProvider storageKeyProvider)
		{
			_storageKeyProvider = storageKeyProvider;
		}

		#endregion

		#region Public Methods

		public string CreateImage(CapthcaConfig config)
		{
			var bitmap = FillBackground(config);

			var filePath = _storageKeyProvider.GetFilePath(config.CaptchaType, ImageFormat.Png);

			SaveImage(bitmap, filePath);

			return _storageKeyProvider.GetFileName(filePath);
		}

		public string CreateImage(string text, CapthcaConfig config = null)
		{
			if (config == null)
			{
				config = new CapthcaConfig();
			}

			var bitmap = FillBackground(config);

			DrawString(bitmap, config, new[] { text });

			var filePath = _storageKeyProvider.GetFilePath(config.CaptchaType, ImageFormat.Png);

			SaveImage(bitmap, filePath);

			return _storageKeyProvider.GetFileName(filePath);
		}

		public string CreateImage(string[] textArray, CapthcaConfig config = null)
		{
			if (config == null)
			{
				config = new CapthcaConfig();
			}

			var bitmap = FillBackground(config);

			DrawString(bitmap, config, textArray);

			var filePath = _storageKeyProvider.GetFilePath(config.CaptchaType, ImageFormat.Png);

			SaveImage(bitmap, filePath);

			return _storageKeyProvider.GetFileName(filePath);
		}

		#endregion

		#region Private Methods

		private void SaveImage(Bitmap bitmap, string path)
		{
			bitmap.Save(path);
		}

		private void SaveImage(Bitmap bitmap, string path, ImageFormat format)
		{
			bitmap.Save(path, format);
		}

		private void DrawString(Bitmap fontBitmap, CapthcaConfig config, string[] codes)
		{
			var fontSize = config.Height * 0.125f;

			var font = new Font(config.FontFamily, fontSize, FontStyle.Italic, GraphicsUnit.Point);

			using (var graphics = Graphics.FromImage(fontBitmap))
			{
				graphics.CompositingQuality = CompositingQuality.AssumeLinear;
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;

				var stringFormat = new StringFormat
				{
					Alignment = StringAlignment.Center,
					LineAlignment = StringAlignment.Center
				};

				var brush = new SolidBrush(config.FontColor);
				var rectangle = new Rectangle(0, 0, config.Width, config.Height);

				foreach (var code in codes)
				{
					graphics.DrawString(code, font, brush, rectangle, stringFormat);
				}

				graphics.Flush();
			}
		}

		private Bitmap FillBackground(CapthcaConfig config)
		{
			var bitmap = new Bitmap(config.Width, config.Height);

			using (var graphics = Graphics.FromImage(bitmap))
			{
				graphics.CompositingQuality = CompositingQuality.AssumeLinear;
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;

				var brush = new SolidBrush(config.BackgroundColor);
				var backRectangle = new Rectangle(0, 0, config.Width, config.Height);

				graphics.FillRectangle(brush, backRectangle);
			}
			return bitmap;
		}

		#endregion
	}
}
