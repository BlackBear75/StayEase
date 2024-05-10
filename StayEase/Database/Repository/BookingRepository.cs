using StayEase.Database.DB;
using StayEase.Database.Interface;

namespace StayEase.Database.Repository
{
	public class BookingRepository : IBookingRepository
	{
        private readonly StayEasyDbContext _db;
        public BookingRepository(StayEasyDbContext db)
        {
            _db = db;
        }
    }
}
