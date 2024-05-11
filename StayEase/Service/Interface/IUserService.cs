using StayEase.Model.Entity.BaseResponce;
using StayEase.Model.Entity.User;
using StayEase.Model.ViewModel;

namespace StayEase.Service.Interface
{
	public interface IUserService
	{
		Task<IBaseResponse<bool>> RegisterUser(UserViewModel entity);

		Task<IBaseResponse<bool>> DeleteUser(Guid id);

		Task<IBaseResponse<UserModel>> GetUser(Guid id);

		Task<IBaseResponse<IEnumerable<UserModel>>> SelectUsers();

		Task<IBaseResponse<UserModel>> UpdateUser(Guid id, UserModel entity);
	}
}
