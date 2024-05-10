using StayEase.Database.Interface;
using StayEase.Service.Interface;

namespace StayEase.Service.Implamentation
{
	public class BookingService: IBookingService
	{
        private readonly IBookingRepository _bookingRepository;
        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
    }
}
