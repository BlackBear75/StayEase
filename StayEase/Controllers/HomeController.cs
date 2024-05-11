using Microsoft.AspNetCore.Mvc;

namespace StayEase.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
