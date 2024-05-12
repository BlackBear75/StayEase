using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StayEase.Model.Entity.User;
using StayEase.Model.ViewModel;
using StayEase.Service.Interface;

namespace StayEase.Controllers
{
	[Route("/api/[controller]")]
	[ApiController]
	public class AccommodationController : Controller
	{
		private readonly IAccommodationService _accommodationService;
		private readonly UserManager<UserModel> _userManager;


		public AccommodationController(IAccommodationService accommodationService, UserManager<UserModel> userManager)
		{
			_accommodationService = accommodationService;
			_userManager = userManager;

		}


		[HttpPost]
		[Route("AddAccommodation")]

		public async Task<IActionResult> AddAccommodation(AccomodationViewModel accomodation)
		{
			try
			{
				var user = await _userManager.GetUserAsync(User);
				var res = await _accommodationService.AddAccommodation(accomodation, Guid.Parse(user.Id));

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
				return BadRequest($"Failed to create accommodation: {ex.Message}");
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetAccommodation(Guid id)
		{
			var accommodation = await _accommodationService.GetAccommodation(id);
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
		[Route("SelectAccommodations")]

		public async Task<IActionResult> SelectAccommodations()
		{
			var accommodations = await _accommodationService.SelectAccommodation();
			if (accommodations.StatusCode != Model.Enum.StatusCode.EntityNull)
			{
				return Ok(accommodations);
			}
			else
			{
				return BadRequest(accommodations.Description);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAccommodation(Guid id, AccomodationViewModel accomodation)
		{
			try
			{
				var updatedaccommodation= await _accommodationService.UpdateAccommodation(id, accomodation);
				if (updatedaccommodation.StatusCode == Model.Enum.StatusCode.EntityNull)
				{

					return BadRequest(updatedaccommodation.Description);
				}
				else
				{
					return Ok(updatedaccommodation.Data);
				}
			}
			catch (Exception ex)
			{
				return BadRequest($"Failed to update accommodation: {ex.Message}");
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAccommodation(Guid id)
		{
			try
			{
				await _accommodationService.DeleteAccommodation(id);
				return Ok("accommodation deleted successfully");


			}
			catch (Exception ex)
			{
				return BadRequest($"Failed to delete accommodation: {ex.Message}");
			}
		}
	}
}
