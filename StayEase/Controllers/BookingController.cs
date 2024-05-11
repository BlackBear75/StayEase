using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StayEase.Model.Entity.User;
using StayEase.Model.ViewModel;
using StayEase.Service.Interface;

namespace StayEase.Controllers
{
	[Route("/api/[controller]")]
	[ApiController]
	public class BookingController : Controller
	{
		private readonly IBookingService _bookingService;
		private readonly UserManager<UserModel> _userManager;


		public BookingController(IBookingService bookingService, UserManager<UserModel> userManager)
		{
			_bookingService = bookingService;
			_userManager = userManager;

		}


		[HttpPost]
		[Route("AddBooking")]

		public async Task<IActionResult> AddBooking(BookingViewModel booking,Guid accommodationid)
		{
			try
			{
				var user = await _userManager.GetUserAsync(User);
				var res = await _bookingService.AddBooking(booking,Guid.Parse(user.Id), accommodationid);

				if (res.Data)
				{
					return Ok(res.Description);
				}
				else
				{
					return BadRequest(res.Description);
				}
			}
			catch (Exception ex)
			{
				return BadRequest($"Failed to create booking: {ex.Message}");
			}
		}
	}
}
