using StayEase.Model.Entity.BaseResponce;
using StayEase.Model.ViewModel;

namespace StayEase.Service.Interface
{
	public interface IBookingService
	{
		Task<IBaseResponse<bool>> AddBooking(BookingViewModel entity, Guid userid, Guid accommodationid);

		Task<IBaseResponse<BookingViewModel>> GetBooking(Guid id);
		Task<IBaseResponse<IEnumerable<BookingViewModel>>> SelectBooking();

		Task<IBaseResponse<bool>> DeleteBooking(Guid id);
		Task<IBaseResponse<bool>> UpdateBooking(Guid id, BookingViewModel entity);
	}
}
