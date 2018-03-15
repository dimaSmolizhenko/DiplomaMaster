﻿using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CaptchaDemo.Core.IoC.Resolver;
using CaptchaDemo.Data.Enum;

namespace CaptchaDemo.Attributes
{
	public class CaptchaAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			if (value != null)
			{
				var captchaWords = value.ToString().Split(' ');
				var context = HttpContext.Current; //TODO: to sessionProvider
				var guid = context.Session["CaptchaGuid"].ToString();
				var type = (CaptchaTypes)context.Session["CaptchaType"];

				if (captchaWords.Any() && !string.IsNullOrEmpty(guid))
				{
					var captchaResolverFactory = new CaptchaResolverFactory();
					var captchaService = captchaResolverFactory.GetServiceByType(type);
					var isValid = captchaService.ValidateCaptchaAsync(guid, captchaWords); 
					return isValid;
				}

			}
			return false;
		}
	}
}