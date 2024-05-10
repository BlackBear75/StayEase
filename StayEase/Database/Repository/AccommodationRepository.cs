using StayEase.Database.DB;
using StayEase.Database.Interface;

namespace StayEase.Database.Repository
{
	public class AccommodationRepository : IAccommodationRepository
	{
        private readonly StayEasyDbContext _db;
        public AccommodationRepository(StayEasyDbContext db)
        {
            _db = db;
        }


    }
}
