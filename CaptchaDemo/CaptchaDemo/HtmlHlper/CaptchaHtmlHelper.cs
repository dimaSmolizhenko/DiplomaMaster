using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using CaptchaDemo.Data.Enum;

namespace CaptchaDemo.HtmlHlper
{
	public static class CaptchaHtmlHelper
	{

		public static IHtmlString Captcha(this HtmlHelper htmlHelper, string name, string refreshLabel)
		{
			return Captcha(htmlHelper, name, refreshLabel);
		}

		public static IHtmlString Captcha(this HtmlHelper htmlHelper, string name, object htmlAttributes)
		{
			return Captcha(htmlHelper, name, null, htmlAttributes);
		}

		public static IHtmlString Captcha(this HtmlHelper htmlHelper, string name, string refreshLabel, object htmlAttributes)
		{
			return CaptchaHelper(htmlHelper, name,  refreshLabel, null);
		}

		public static IHtmlString CaptchaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
		{
			return CaptchaFor(htmlHelper, expression, null);
		}

		public static IHtmlString CaptchaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, 
			object htmlAttributes)
		{
			return CaptchaFor(htmlHelper, expression, null, htmlAttributes);
		}

		public static IHtmlString CaptchaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,
			 string refreshLabel)
		{
			return CaptchaFor(htmlHelper, expression,  refreshLabel, null);
		}

		public static IHtmlString CaptchaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, 
			string refreshLabel, object htmlAttributes)
		{
			return CaptchaHelper(htmlHelper, ExpressionHelper.GetExpressionText(expression), refreshLabel, htmlAttributes);
		}

		private static IHtmlString CaptchaHelper(this HtmlHelper htmlHelper, string name, string refreshLabel, object htmlAttributes)
		{
			var container = new TagBuilder("div");
			container.MergeAttribute("class", "captcha-container");

			var image = new TagBuilder("img");
			image.MergeAttribute("src", "../storage/GameWords/847520d4-c130-43df-a909-76d5ff831088.png");
			image.MergeAttribute("border", "0");
			image.MergeAttribute("class","captcha-image");

			container.InnerHtml = image.ToString(TagRenderMode.SelfClosing);

			var refresh = new TagBuilder("a");
			refresh.MergeAttribute("href", "javascript:void(0)");
			refresh.MergeAttribute("class", "refresh-captcha");
			refresh.SetInnerText(refreshLabel);

			//var br = new TagBuilder("br");

			var textBox = htmlHelper.TextBox(name, "", htmlAttributes);

			return new HtmlString(
				container.ToString(TagRenderMode.Normal) + Environment.NewLine +
				refresh.ToString(TagRenderMode.Normal) + Environment.NewLine +
				//br.ToString(TagRenderMode.SelfClosing) + Environment.NewLine +
				textBox + Environment.NewLine);
		}

	}
}