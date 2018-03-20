using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CaptchaDemo.Core.Data.BussinessModels;
using CaptchaDemo.Core.Data.Enum;
using CaptchaDemo.Core.IoC.Resolver;

namespace CaptchaDemo.HtmlHlper
{
	public static class CaptchaHtmlHelper
	{
		public static IHtmlString Captcha(this HtmlHelper htmlHelper, string name, CaptchaTypes type, string refreshLabel)
		{
			return Captcha(htmlHelper, name, type, refreshLabel, null);
		}

		public static IHtmlString Captcha(this HtmlHelper htmlHelper, string name, CaptchaTypes type, object htmlAttributes)
		{
			return Captcha(htmlHelper, name, type, null, htmlAttributes);
		}

		public static IHtmlString Captcha(this HtmlHelper htmlHelper, string name, CaptchaTypes type, string refreshLabel, object htmlAttributes)
		{
			return CaptchaHelper(htmlHelper, name, type,  refreshLabel, htmlAttributes);
		}

		public static IHtmlString CaptchaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, CaptchaTypes type)
		{
			return CaptchaFor(htmlHelper, expression,type, null);
		}

		public static IHtmlString CaptchaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, 
			CaptchaTypes type, object htmlAttributes)
		{
			return CaptchaFor(htmlHelper, expression, type, null, htmlAttributes);
		}

		public static IHtmlString CaptchaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, 
			CaptchaTypes type, string refreshLabel)
		{
			return CaptchaFor(htmlHelper, expression, type,  refreshLabel, null);
		}

		public static IHtmlString CaptchaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, 
			CaptchaTypes type, string refreshLabel, object htmlAttributes)
		{
			return CaptchaHelper(htmlHelper, ExpressionHelper.GetExpressionText(expression), type, refreshLabel, htmlAttributes);
		}

		private static IHtmlString CaptchaHelper(this HtmlHelper htmlHelper, string name, CaptchaTypes type, string refreshLabel, object htmlAttributes)
		{
			var container = new TagBuilder("div");
			container.MergeAttribute("class", "captcha-container");

			var question = GetQuestion(type);

			var context = HttpContext.Current; //TODO: to sessionProvider
			context.Session["CaptchaGuid"] = question.QuestionId;
			context.Session["CaptchaType"] = type;

			if (type != CaptchaTypes.GameWords)
			{
				var span = new TagBuilder("div");
				span.MergeAttribute("class", "captcha-label");
				span.SetInnerText(question.Text);
				container.InnerHtml += span.ToString(TagRenderMode.Normal);
			}

			var image = new TagBuilder("img");
			image.MergeAttribute("src", question.ImageUrl);
			var className = type == CaptchaTypes.RebusMath ? "type-math" : "";
			image.MergeAttribute("class", $"captcha-image {className}");

			container.InnerHtml += image.ToString(TagRenderMode.SelfClosing);

			var refresh = new TagBuilder("a"); //TODO: add refresh captcha
			refresh.MergeAttribute("href", "javascript:void(0)");
			refresh.MergeAttribute("class", "refresh-captcha");
			refresh.SetInnerText(refreshLabel);

			var textBox = htmlHelper.TextBox(name, "", htmlAttributes);

			return new HtmlString(
				container.ToString(TagRenderMode.Normal) + Environment.NewLine +
				refresh.ToString(TagRenderMode.Normal) + Environment.NewLine +
				textBox + Environment.NewLine);
		}

		private static QuestionModel GetQuestion(CaptchaTypes type)
		{
			var captchaResolverFactory = new CaptchaResolverFactory();
			var captchaService = captchaResolverFactory.GetServiceByType(type);
			var question = captchaService.GetCaptha();

			return question;
		}

	}
}