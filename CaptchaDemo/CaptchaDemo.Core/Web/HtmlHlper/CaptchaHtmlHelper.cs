using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using CaptchaDemo.Core.Data.BussinessModels;
using CaptchaDemo.Core.Data.Enum;
using CaptchaDemo.Core.IoC.Resolver;
using Newtonsoft.Json;

namespace CaptchaDemo.Core.Web.HtmlHlper
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

		public static IHtmlString Captcha(this HtmlHelper htmlHelper, string name, CaptchaTypes type, string refreshLabel,
			object htmlAttributes)
		{
			return CaptchaHelper(htmlHelper, name, type, refreshLabel, htmlAttributes);
		}

		public static IHtmlString CaptchaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, TProperty>> expression, CaptchaTypes type)
		{
			return CaptchaFor(htmlHelper, expression, type, null);
		}

		public static IHtmlString CaptchaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, TProperty>> expression,
			CaptchaTypes type, object htmlAttributes)
		{
			return CaptchaFor(htmlHelper, expression, type, null, htmlAttributes);
		}

		public static IHtmlString CaptchaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, TProperty>> expression,
			CaptchaTypes type, string refreshLabel)
		{
			return CaptchaFor(htmlHelper, expression, type, refreshLabel, null);
		}

		public static IHtmlString CaptchaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, TProperty>> expression,
			CaptchaTypes type, string refreshLabel, object htmlAttributes)
		{
			return CaptchaHelper(htmlHelper, ExpressionHelper.GetExpressionText(expression), type, refreshLabel, htmlAttributes);
		}

		private static IHtmlString CaptchaHelper(this HtmlHelper htmlHelper, string name, CaptchaTypes type,
			string refreshLabel, object htmlAttributes)
		{
			var question = GetQuestion(type);

			var context = HttpContext.Current; //TODO: to sessionProvider
			context.Session["CaptchaGuid"] = question.QuestionId;
			context.Session["CaptchaType"] = type;

			var container = type == CaptchaTypes.Rebus ? GetCaptchaFigures(question) : GetCaptchaContainer(question, type);

			var refresh = new TagBuilder("a"); //TODO: add refresh captcha
			refresh.MergeAttribute("href", "javascript:void(0)");
			refresh.MergeAttribute("class", "refresh-captcha");
			refresh.SetInnerText(refreshLabel);

			var defaultHtmlAttributes = type == CaptchaTypes.Rebus ? new { type = "hidden", id = "captchaAnswerInput" } : new object();
			var mergedHtmlAttributes = MergeHtmlAttributes(htmlAttributes, defaultHtmlAttributes);
			var textBox = htmlHelper.TextBox(name, "", mergedHtmlAttributes);

			return new HtmlString(
				container + Environment.NewLine +
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

		private static string GetCaptchaContainer(QuestionModel question, CaptchaTypes type)
		{
			var container = new TagBuilder("div");
			container.MergeAttribute("class", "captcha-container");

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

			return container.ToString(TagRenderMode.Normal);
		}

		private static string GetCaptchaFigures(QuestionModel question)
		{
			var answers = JsonConvert.DeserializeObject<List<ValuePair>>(question.Answers.FirstOrDefault());
			var container = new TagBuilder("div");
			container.MergeAttribute("class", $"game-field game-field-{answers.Count}");

			for (var i = 0; i < answers.Count; i++)
			{
				var row = GetFieldRow(answers[i].StartValue, answers[i].EndValue);
				container.InnerHtml += row;
			}

			return container.ToString(TagRenderMode.Normal);
		}

		private static string GetFieldColumn(string className, string id)
		{
			var column = new TagBuilder("div");
			column.MergeAttribute("class", $"field-column {className}");

			var canvas = new TagBuilder("canvas");
			canvas.MergeAttribute("id", id);

			column.InnerHtml += canvas.ToString(TagRenderMode.Normal);

			return column.ToString(TagRenderMode.Normal);
		}

		private static string GetFieldRow(string idStart, string idEnd)
		{
			var row = new TagBuilder("div");
			row.MergeAttribute("class", "field-row");

			var startColumn = GetFieldColumn("field-column__start", idStart);
			var endColumn = GetFieldColumn("field-column__end", idEnd);

			row.InnerHtml += startColumn;
			row.InnerHtml += endColumn;

			return row.ToString(TagRenderMode.Normal);
		}

		private static IDictionary<string, object> MergeHtmlAttributes(object htmlAttributesObject, object defaultHtmlAttributesObject)
		{
			var concatKeys = new string[] { "class" };

			var htmlAttributesDict = htmlAttributesObject as IDictionary<string, object>;
			var defaultHtmlAttributesDict = defaultHtmlAttributesObject as IDictionary<string, object>;

			RouteValueDictionary htmlAttributes = (htmlAttributesDict != null)
				? new RouteValueDictionary(htmlAttributesDict)
				: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributesObject);
			RouteValueDictionary defaultHtmlAttributes = (defaultHtmlAttributesDict != null)
				? new RouteValueDictionary(defaultHtmlAttributesDict)
				: HtmlHelper.AnonymousObjectToHtmlAttributes(defaultHtmlAttributesObject);

			foreach (var item in htmlAttributes)
			{
				if (concatKeys.Contains(item.Key))
				{
					defaultHtmlAttributes[item.Key] = (defaultHtmlAttributes[item.Key] != null)
						? string.Format("{0} {1}", defaultHtmlAttributes[item.Key], item.Value)
						: item.Value;
				}
				else
				{
					defaultHtmlAttributes[item.Key] = item.Value;
				}
			}

			return defaultHtmlAttributes;
		}
	}
}