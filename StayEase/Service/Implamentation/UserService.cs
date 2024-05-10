using StayEase.Database.Interface;
using StayEase.Service.Interface;

namespace StayEase.Service.Implamentation
{
	public class UserService : IUserService
	{
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
			_userRepository = userRepository;
        }


    }
}
