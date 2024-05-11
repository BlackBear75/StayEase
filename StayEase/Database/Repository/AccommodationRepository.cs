using Microsoft.EntityFrameworkCore;
using StayEase.Database.DB;
using StayEase.Database.Interface;
using StayEase.Model.Entity.AccommodationModel;
using StayEase.Model.Entity.BaseResponce;

namespace StayEase.Database.Repository
{
	public class AccommodationRepository : IAccommodationRepository
	{
        private readonly StayEasyDbContext _db;
        public AccommodationRepository(StayEasyDbContext db)
        {
            _db = db;
        }

		public async  Task<bool> AddAccommodation(AccommodationModel entity)
		{
			try
			{

				_db.AccommodationModels.Add(entity);
				await _db.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<bool> DeleteAccommodation(AccommodationModel entity)
		{
			try
			{
				_db.AccommodationModels.Remove(entity);

				await _db.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<AccommodationModel> GetAccommodation(Guid id)
		{
			return await _db.AccommodationModels.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<IEnumerable<AccommodationModel>> SelectAccommodation()
		{
			return await _db.AccommodationModels.ToListAsync();
		}

		public async Task<bool> UpdateAccommodation(AccommodationModel entity)
		{
			try
			{
				_db.AccommodationModels.Update(entity);
				await _db.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
