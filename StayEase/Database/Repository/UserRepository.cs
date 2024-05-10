using StayEase.Database.DB;
using StayEase.Database.Interface;

namespace StayEase.Database.Repository
{
	public class UserRepository : IUserRepository
	{
        private readonly StayEasyDbContext _db;
        public UserRepository(StayEasyDbContext db)
        {
            _db = db;
        }


    }
}
