using System.Threading.Tasks;
using System.Web.Mvc;
using CaptchaDemo.Configuration;
using CaptchaDemo.Core.Models;
using CaptchaDemo.Core.Services;
using CaptchaDemo.Data.Entities;
using CaptchaDemo.Data.Enum;
using CaptchaDemo.Data.Repositories;
using CaptchaDemo.IoC.Resolver;
using CaptchaDemo.Models;

namespace CaptchaDemo.Controllers
{
	public class HomeController : Controller
	{
		private readonly IRepository<Question> _repository;
		private readonly IImageService _imageService;
		private readonly ICaptchaConfiguration _captchaConfiguration;
		private readonly IFileService _fileService;

		public HomeController(IRepository<Question> repository, IImageService imageService, ICaptchaConfiguration captchaConfiguration, IFileService fileService)
		{
			_repository = repository;
			_imageService = imageService;
			_captchaConfiguration = captchaConfiguration;
			_fileService = fileService;
		}

		public ActionResult Index()
		{
			//_fileService.GetWordsFromFile();

			//var service = _captchaResolverFactory.GetServiceByType(CaptchaTypes.GameWords);
			//var question = Task.Run(async () => await service.GetCapthaAsync()).Result;

			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = $"Your application description page.";

			//var captcha = Task.Run(async () => await _capthcaService.GetCapthaAsync()).Result;

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			Session["CaptchaGuid"] = "5a9491b1d2b5e105b003b6ee";
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
	}
}