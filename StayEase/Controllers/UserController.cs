using Microsoft.AspNetCore.Mvc;

namespace StayEase.Controllers
{
	public class UserController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
