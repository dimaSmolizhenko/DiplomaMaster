﻿using CaptchaDemo.Core.Models;

namespace CaptchaDemo.Core.Services
{
	public interface IImageService
	{
		string CreateImage(CapthcaConfig config);
		string CreateImage(string text, CapthcaConfig config = null);
		string CreateImage(string[] text, CapthcaConfig config = null);
	}
}
