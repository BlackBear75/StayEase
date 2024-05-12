using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StayEase.Model.Entity.User;
using StayEase.Model.ViewModel;
using StayEase.Service.Implamentation;
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

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBooking(Guid id)
		{
			try
			{
				await _bookingService.DeleteBooking(id);
				return Ok("Booking deleted successfully");


			}
			catch (Exception ex)
			{
				return BadRequest($"Failed to delete Booking: {ex.Message}");
			}
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetBooking(Guid id)
		{
			var accommodation = await _bookingService.GetBooking(id);
			if (accommodation.Data != null)
			{
				return Ok(accommodation);
			}
			else
			{
				return BadRequest(accommodation.Description);
			}
		}

		[HttpGet]
		[Route("SelectBookings")]
		public async Task<IActionResult> SelectBookings()
		{
			var bookings = await _bookingService.SelectBooking();
			if (bookings.StatusCode != Model.Enum.StatusCode.EntityNull)
			{
				return Ok(bookings);
			}
			else
			{
				return BadRequest(bookings.Description);
			}
		}


		[HttpGet]
		[Route("SelectUserBookings")]
		public async Task<IActionResult> SelectUserBookings()
		{
			var user = await _userManager.GetUserAsync(User);
			var bookings = await _bookingService.SelectUserBooking(Guid.Parse(user.Id));
			if (bookings.StatusCode != Model.Enum.StatusCode.EntityNull)
			{
				return Ok(bookings);
			}
			else
			{
				return BadRequest(bookings.Description);
			}
		}
		

	}
}
