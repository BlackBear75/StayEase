

using StayEase.Model.Entity.AccommodationModel;
using StayEase.Model.Entity.Booking;
using StayEase.Model.ViewModel;

namespace StayEase.Database.Interface
{
	public interface IBookingRepository
	{
		Task<bool> AddBooking(BookingModel entity);

		Task<bool> IsAccommodationAvailable(Guid accommodationId, BookingViewModel bookingView);
		Task<BookingModel> GetBooking(Guid id);
		Task<IEnumerable<BookingModel>> SelectBooking();
	
		Task<IEnumerable<BookingModel>> SelectUserBooking(Guid id);

		Task<List<DateTime>> GetAvailableDates(Guid accommodationId, BookingViewModel bookingView);
		Task<bool> DeleteBooking(BookingModel entity);
		
	}
}
