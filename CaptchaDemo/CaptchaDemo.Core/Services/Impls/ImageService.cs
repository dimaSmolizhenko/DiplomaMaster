using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using CaptchaDemo.Core.Data.BussinessModels;
using CaptchaDemo.Core.Data.Enum;
using CaptchaDemo.Core.Models;

namespace CaptchaDemo.Core.Services.Impls
{
	public class ImageService : IImageService
	{
		#region Dependencies

		private readonly IStorageKeyProvider _storageKeyProvider;
		private readonly IRandomProvider _randomProvider;

		#endregion

		#region .ctor

		public ImageService(IStorageKeyProvider storageKeyProvider, IRandomProvider randomProvider)
		{
			_storageKeyProvider = storageKeyProvider;
			_randomProvider = randomProvider;
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

			SaveImage(bitmap, filePath, ImageFormat.Png);

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

		public PuzzleMathModel CreateImageFromIcon(string imagePath, CapthcaConfig config = null)
		{
			if (config == null)
			{
				config = new CapthcaConfig
				{
					CaptchaType = CaptchaTypes.PuzzleMath.ToString(),
					BackgroundColor = Color.White
				};
			}

			var background = FillBackground(config);

			var imageBitmap = new Bitmap(imagePath);

			var objectCount = DrawImage(background, imageBitmap);

			var filePath = _storageKeyProvider.GetFilePath(config.CaptchaType, ImageFormat.Png);

			SaveImage(background, filePath);
			var memory = new MemoryStream();
			background.Save(memory, ImageFormat.Png);

			return new PuzzleMathModel
			{
				ImageUrl = _storageKeyProvider.GetFileName(filePath),
				Answer = objectCount.ToString()
			};
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
				SetGraphicsQuality(graphics);

				var brush = new SolidBrush(config.BackgroundColor);
				var backRectangle = new Rectangle(0, 0, config.Width, config.Height);

				graphics.FillRectangle(brush, backRectangle);
			}
			return bitmap;
		}

		private int DrawImage(Bitmap background, Bitmap drawImage)
		{
			var count = 0;
			using (var graphics = Graphics.FromImage(background))
			{
				SetGraphicsQuality(graphics);

				var marginTop = background.Height / 2 - drawImage.Height > 0 ? _randomProvider.GetRandom(background.Height / 2 - drawImage.Height) : 0;
				var rows = background.Height / (drawImage.Height + marginTop);
				var rowMargin = marginTop;
				for (var i = 0; i < rows; i++)
				{
					var marginLeft = background.Width / 2 - drawImage.Width > 0 ? _randomProvider.GetRandom(background.Width / 2 - drawImage.Width) : 0;
					var colMargin = marginLeft;
					var columns = background.Width / (drawImage.Width + colMargin);
					for (var j = 0; j < columns; j++)
					{
						graphics.DrawImage(drawImage, colMargin, rowMargin,  drawImage.Width, drawImage.Height);
						colMargin += marginLeft + drawImage.Width;
						count++;
					}
					rowMargin += marginTop + drawImage.Height;
				}
			}

			return count;
		}

		private void SetGraphicsQuality(Graphics graphics)
		{
			graphics.CompositingQuality = CompositingQuality.AssumeLinear;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
		}

		#endregion
	}
}
