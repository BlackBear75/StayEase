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
	public class UserController : Controller
	{
		private readonly IUserService _userService;
		private readonly UserManager<UserModel> _userManager;
		private readonly SignInManager<UserModel> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public UserController(IUserService UserService, UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, RoleManager<IdentityRole> roleManager)
		{
			_userService = UserService;
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}


		
		[HttpPost]
		[Route("RegisterUser")]
		public async Task<IActionResult> RegisterUser(UserViewModel model)
		{
			var result = await _userService.RegisterUser(model);

			if (result.StatusCode == Model.Enum.StatusCode.OK && result.Data)
			{
				return Ok("User registered successfully");
			}
			else
			{
				return BadRequest(result.Description);
			}
		}
		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

				if (result.Succeeded)
				{
					return Ok("Login Succeeded");
				}

				ModelState.AddModelError(string.Empty, "Invalid login attempt");
			}

			return BadRequest("Login no Succeeded");
		}


		[HttpDelete("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteUser(Guid id)
		{
			try
			{

				await _userService.DeleteUser(id);
				return Ok("User deleted successfully");


			}
			catch (Exception ex)
			{
				return BadRequest($"Failed to delete user: {ex.Message}");
			}
		}


		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(Guid id, UserModel user)
		{
			try
			{

				var updatedUser = await _userService.UpdateUser(id, user);
				if (updatedUser.StatusCode == Model.Enum.StatusCode.EntityNull)
				{

					return BadRequest(updatedUser.Description);
				}
				else
				{
					return Ok(updatedUser.Data);
				}
			}
			catch (Exception ex)
			{
				return BadRequest($"Failed to update user: {ex.Message}");
			}
		}


		[HttpGet]
		[Route("GetUsers")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetAllUsers()
		{
			var users = await _userService.SelectUsers();
			if (users.StatusCode != Model.Enum.StatusCode.EntityNull)
			{
				return Ok(users);
			}
			else
			{
				return BadRequest(users.Description);
			}
		}


		[HttpGet("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetUserById(Guid id)
		{
			var user = await _userService.GetUser(id);
			if (user.Data != null)
			{
				return Ok(user);
			}
			else
			{
				return BadRequest(user.Description);
			}
		}

		[HttpPost]
		[Route("Logout")]
		public async Task<IActionResult> Logout()
		{
		     await _signInManager.SignOutAsync();
			return Ok("Logout succesful");
		}


	}
}
