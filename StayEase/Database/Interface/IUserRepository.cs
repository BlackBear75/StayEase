using StayEase.Model.Entity.User;

namespace StayEase.Database.Interface
{
	public interface IUserRepository
	{
		Task<UserModel> Get(Guid id);

		Task<bool> Delete(UserModel entity);



		Task<List<UserModel>> Select();
		Task<UserModel> Update(UserModel entity);
	}
}
