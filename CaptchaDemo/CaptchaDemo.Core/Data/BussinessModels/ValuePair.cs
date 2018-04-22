using Newtonsoft.Json;

namespace CaptchaDemo.Core.Data.BussinessModels
{
	public class ValuePair
	{
		[JsonProperty("from")]
		public string StartValue { get; set; }

		[JsonProperty("to")]
		public string EndValue { get; set; }
	}
}
