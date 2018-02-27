using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CaptchaDemo.Attributes;

namespace CaptchaDemo.Models
{
	public class ContactViewMode
	{
		public string Title { get; set; }

		[Captcha]
		public string Captcha { get; set; }

		public int Page { get; set; }
	}
}