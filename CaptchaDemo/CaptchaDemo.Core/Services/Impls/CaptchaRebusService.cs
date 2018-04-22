using System.Collections.Generic;
using System.Linq;
using CaptchaDemo.Core.Configuration;
using CaptchaDemo.Core.Data.BussinessModels;
using CaptchaDemo.Core.Data.Entities;
using CaptchaDemo.Core.Data.Enum;
using CaptchaDemo.Core.IoC.Resolver;
using Newtonsoft.Json;

namespace CaptchaDemo.Core.Services.Impls
{
	public class CaptchaRebusService : BaseCaptchaService, ICapthcaService
	{
		private readonly Dictionary<int, string> _startValues = new Dictionary<int, string>
		{
			{1, "figure-rectangle" },
			{2, "figure-round" },
			{3, "figure-rhomb" }
		};

		private readonly Dictionary<int, string> _endValues = new Dictionary<int, string>
		{
			{1, "figure-rectangle-bord" },
			{2, "figure-round-bord" },
			{3, "figure-rhomb-bord" }
		};

		private readonly IRandomProvider _randomProvider;

		#region .ctor

		public CaptchaRebusService(IStorageKeyProvider storageKeyProvider, ICaptchaConfiguration captchaConfiguration,
			ICaptchaResolverFactory captchaResolverFactory, IRandomProvider randomProvider)
			: base(storageKeyProvider, captchaConfiguration, captchaResolverFactory)
		{
			_randomProvider = randomProvider;
		}

		#endregion

		#region Public Methods

		public bool ValidateCaptcha(string guid, string answer)
		{
			var answers = JsonConvert.DeserializeObject<List<ValuePair>>(answer);
			return CheckAnswers(answers);
		}

		public QuestionModel GetCaptha()
		{
			var limit = _startValues.Count < _endValues.Count ? _startValues.Count : _endValues.Count;
			var rowCount = _randomProvider.GetRandom(2, limit + 1);

			var randomIds = GetRandomIds(rowCount);
			var question = CreateQuestion(JsonConvert.SerializeObject(randomIds));

			return MapQuestionToQuestionModel(question);
		}

		#endregion

		#region Private Methods

		private Question CreateQuestion(string answer)
		{
			return new Question
			{
				Id = CaptchaStorageProvider.CreateIdentifier(),
				ImageUrl = null,
				Answers = new string[] { answer },
				Text = "Please connect figures!",
				Type = CaptchaTypes.PuzzleMath.ToString()
			};
		}

		private List<ValuePair> GetRandomValues(IList<ValuePair> values, int count)
		{
			var randomValues = new List<ValuePair>();

			for (var index = 0; index < count; index++)
			{
				var valueKey = _randomProvider.GetRandom(0, values.Count);
				var randomItem = values[valueKey];
				randomValues.Add(randomItem);
				values.Remove(randomItem);
			}

			return randomValues;
		}

		private IEnumerable<ValuePair> GetRandomIds(int count)
		{
			var list = new List<ValuePair>();

			var startValues = GetRandomValues(_startValues.Select(x => new ValuePair
			{
				StartValue = x.Key.ToString(),
				EndValue = x.Value
			}).ToList(), count);
			var filteredValues = _endValues.Where(x => startValues.Any(s => s.StartValue.Equals(x.Key.ToString())));

			var endValues = GetRandomValues(filteredValues.Select(x => new ValuePair
			{
				StartValue = x.Key.ToString(),
				EndValue = x.Value
			}).ToList(), count);

			for (var i = 0; i < count; i++)
			{
				list.Add(new ValuePair
				{
					StartValue = startValues[i].EndValue,
					EndValue = endValues[i].EndValue
				});
			}

			return list;
		}

		private bool CheckAnswers(IList<ValuePair> randomValues)
		{
			foreach (var item in randomValues)
			{
				var startValue = _startValues.FirstOrDefault(x => x.Value.Equals(item.StartValue) || x.Value.Equals(item.EndValue));
				var endValue = _endValues.FirstOrDefault(x => x.Value.Equals(item.StartValue) || x.Value.Equals(item.EndValue));

				if (endValue.Equals(default(KeyValuePair<int, string>)) 
					|| startValue.Equals(default(KeyValuePair<int, string>)) 
					|| !endValue.Key.Equals(startValue.Key))
				{
					return false;
				}
			}
			return true;
		}

		#endregion
	}
}
