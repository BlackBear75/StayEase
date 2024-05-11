using Microsoft.AspNetCore.Identity;
using StayEase.Database.Interface;
using StayEase.Model.Entity.BaseResponce;
using StayEase.Model.Entity.User;
using StayEase.Model.Enum;
using StayEase.Model.ViewModel;
using StayEase.Service.Interface;

namespace StayEase.Service.Implamentation
{
	public class UserService : IUserService
	{
		private readonly UserManager<UserModel> _userManager;
		
		private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository, UserManager<UserModel> userManager)
        {
			_userManager = userManager;
			_userRepository = userRepository;
        }

		public async Task<IBaseResponse<bool>> DeleteUser(Guid id)
		{
			var baseResponse = new BaseResponse<bool>();
			try
			{
				var user = await _userRepository.Get(id);
				if (user == null)
				{
					baseResponse.StatusCode = StatusCode.EntityNull;
					baseResponse.Data = false;
					return baseResponse;
				}


				await _userRepository.Delete(user);

				baseResponse.Description = "DeleteUser succcsesful";
				baseResponse.StatusCode = StatusCode.OK;
				baseResponse.Data = true;
			}
			catch (Exception ex)
			{
				return new BaseResponse<bool>()
				{
					Description = $"[DeleteUser] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
			return baseResponse;
		}

		public async Task<IBaseResponse<UserModel>> GetUser(Guid id)
		{
			var baseResponse = new BaseResponse<UserModel>();
			try
			{
				var task = await _userRepository.Get(id);

				if (task == null)
				{
					baseResponse.StatusCode = StatusCode.EntityNull;
					baseResponse.Description = "NullEntity";
					return baseResponse;
				}

				baseResponse.Description = "Get user succesful";
				baseResponse.StatusCode = StatusCode.OK;
				baseResponse.Data = task;
			}
			catch (Exception ex)
			{
				return new BaseResponse<UserModel>()
				{
					Description = $"[GetUser] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
			return baseResponse;
		}
		public async Task<IBaseResponse<bool>> RegisterUser(UserViewModel entity)
		{
			var baseResponse = new BaseResponse<bool>();
			try
			{
				var existingUser = await _userManager.FindByEmailAsync(entity.Email);
				if (existingUser != null)
				{
					baseResponse.StatusCode = StatusCode.EntityNull;
					baseResponse.Description = " with this email already exists";
					baseResponse.Data = false;
					return baseResponse;
				}
		
				var user = new UserModel
				{
					Email = entity.Email,
					PhoneNumber = entity.PhoneNumber,
					UserName = entity.UserName,
					
				};

				var result = await _userManager.CreateAsync(user, entity.Password);

				if (result.Succeeded)
				{
					baseResponse.StatusCode = StatusCode.OK;
					baseResponse.Data = true;
					baseResponse.Description = " registered successfully";
				}
				else
				{
					baseResponse.StatusCode = StatusCode.BadRequest;
					baseResponse.Description = " registration failed";
					baseResponse.Data = false;
				}
			}
			catch (Exception ex)
			{
				return new BaseResponse<bool>()
				{
					Description = $"[RegisterClient] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
			return baseResponse;
		}

		public async Task<IBaseResponse<IEnumerable<UserModel>>> SelectUsers()
		{
			var baseResponse = new BaseResponse<IEnumerable<UserModel>>();
			try
			{
				var tasks = await _userRepository.Select();
				if (tasks.Count == 0)
				{
					baseResponse.StatusCode = StatusCode.EntityNull;
					baseResponse.Description = "NullEntity";
					return baseResponse;
				}

				baseResponse.Description = "SelectUsers succesful";
				baseResponse.StatusCode = StatusCode.OK;
				baseResponse.Data = tasks;
			}
			catch (Exception ex)
			{
				return new BaseResponse<IEnumerable<UserModel>>()
				{
					Description = $"[SelectUsers] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
			return baseResponse;
		}

		public async Task<IBaseResponse<UserModel>> UpdateUser(Guid id, UserModel entity)
		{
			var baseResponse = new BaseResponse<UserModel>();
			try
			{
				var user = await _userRepository.Get(id);
				if (user == null)
				{
					baseResponse.StatusCode = StatusCode.EntityNull;
					baseResponse.Description = "Not find user his id";
					return baseResponse;
				}

				user.UserName = entity.UserName;

				//work

				var res = await _userRepository.Update(user);
				if (res != null)
				{
					baseResponse.StatusCode = StatusCode.OK;
					baseResponse.Data = user;
					return baseResponse;
				}
				else
				{
					baseResponse.StatusCode = StatusCode.NoUpdate;
					baseResponse.Description = "NoUpdate";
					return baseResponse;
				}

			}
			catch (Exception ex)
			{
				return new BaseResponse<UserModel>()
				{
					Description = $"[UpdateUser] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
		}
	}
}
