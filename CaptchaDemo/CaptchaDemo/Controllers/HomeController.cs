using System.Threading.Tasks;
using System.Web.Mvc;
using CaptchaDemo.Core.Services;
using CaptchaDemo.Data.Entities;
using CaptchaDemo.Data.Enum;
using CaptchaDemo.Data.Repositories;
using CaptchaDemo.Models;

namespace CaptchaDemo.Controllers
{
	public class HomeController : Controller
	{
		private readonly IStorageKeyProvider _storageKeyProvider;
		private readonly IRepository<Question> _repository;

		public HomeController(IStorageKeyProvider storageKeyProvider, IRepository<Question> repository)
		{
			_storageKeyProvider = storageKeyProvider;
			_repository = repository;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View(new ContactViewMode());
		}

		[HttpPost]
		public ActionResult Contact(ContactViewMode model)
		{
			if (ModelState.IsValid)
			{
				return View("Index");
			}
			return View(model);
		}

		public ActionResult Configuration()
		{
			return View(new QuestionViewModel());
		}

		[HttpPost]
		public ActionResult Configuration(QuestionViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			SaveQuestion(model);

			return View();
		}

		private void SaveQuestion(QuestionViewModel model)
		{
			var fileName = model.File.FileName;
			var path = _storageKeyProvider.GetFilePath(fileName, CaptchaTypes.RebusMath.ToString(), true);
			model.File.SaveAs(path);

			var question = new Question
			{
				ImageUrl = _storageKeyProvider.GetFileName(path),
				Answers = new []{model.Answer},
				Text = model.Text,
				Type = CaptchaTypes.RebusMath.ToString()
			};

			Task.Run(async () => await _repository.InsertAsync(question));
		}
	}
}