using Microsoft.AspNetCore.Mvc;

namespace StayEase.Controllers
{
	public class BookingController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
