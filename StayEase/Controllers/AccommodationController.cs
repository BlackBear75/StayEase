using Microsoft.AspNetCore.Mvc;

namespace StayEase.Controllers
{
	public class AccommodationController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
