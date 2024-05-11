using Microsoft.EntityFrameworkCore;
using StayEase.Database.DB;
using StayEase.Database.Interface;
using StayEase.Model.Entity.User;

namespace StayEase.Database.Repository
{
	public class UserRepository : IUserRepository
	{
        private readonly StayEasyDbContext _db;
        public UserRepository(StayEasyDbContext db)
        {
            _db = db;
        }

		public async Task<bool> Delete(UserModel entity)
		{
			try
			{
				_db.Users.Remove(entity);

				await _db.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public async Task<List<UserModel>> Select()
		{
			return await _db.Users.ToListAsync();
		}
		public async Task<UserModel> Update(UserModel entity)
		{
			try
			{
				_db.Users.Update(entity);
				await _db.SaveChangesAsync();
				return entity;
			}
			catch (Exception)
			{
				return null;
			}
		}
		public async Task<UserModel> Get(Guid id)
		{

			return await _db.Users.FirstOrDefaultAsync(x => x.Id == id.ToString());

		}
	}
}
