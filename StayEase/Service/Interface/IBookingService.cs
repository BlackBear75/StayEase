using StayEase.Model.Entity.BaseResponce;
using StayEase.Model.Entity.Booking;
using StayEase.Model.ViewModel;

namespace StayEase.Service.Interface
{
	public interface IBookingService
	{
		Task<IBaseResponse<bool>> AddBooking(BookingViewModel entity, Guid userid, Guid accommodationid);

		Task<IBaseResponse<AccommodatiowithBookingModel>> GetBooking(Guid id);
		Task<IBaseResponse<IEnumerable<AccommodatiowithBookingModel>>> SelectBooking();
		Task<IBaseResponse<IEnumerable<AccommodatiowithBookingModel>>> SelectUserBooking(Guid id);
		Task<IBaseResponse<bool>> DeleteBooking(Guid id);
		
	}
}
