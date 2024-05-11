using StayEase.Database.Interface;
using StayEase.Database.Repository;
using StayEase.Model.Entity.AccommodationModel;
using StayEase.Model.Entity.BaseResponce;
using StayEase.Model.Entity.Booking;
using StayEase.Model.Enum;
using StayEase.Model.ViewModel;
using StayEase.Service.Interface;
using System;
using System.Text;

namespace StayEase.Service.Implamentation
{
	public class BookingService: IBookingService
	{
        private readonly IBookingRepository _bookingRepository;
        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

		public async Task<IBaseResponse<bool>> AddBooking(BookingViewModel entity, Guid userid,Guid accommodationid)
		{
			var baseResponse = new BaseResponse<bool>();
			try
			{
				if (entity == null)
				{
					baseResponse.StatusCode = StatusCode.EntityNull;
					baseResponse.Description = "AddBooking = null";
					baseResponse.Data = false;
					return baseResponse;
				}
				bool Isavailable = await _bookingRepository.IsAccommodationAvailable(accommodationid, entity);

				if (Isavailable)
				{
					var model = new BookingModel();
					model.StartDate = entity.StartDate;
					model.EndDate = entity.EndDate;
					model.UserId = userid;
					model.AccommodationId = accommodationid;
					model.Id = Guid.NewGuid();

					await _bookingRepository.AddBooking(model);

					baseResponse.StatusCode = StatusCode.OK;
					baseResponse.Description = "AddBooking succesful";
					baseResponse.Data = true;
				}
				else
				{
					var availableDates = await _bookingRepository.GetAvailableDates(accommodationid, entity);
					if(availableDates.Count>0)
					{
						baseResponse.StatusCode = StatusCode.OK;

						StringBuilder result = new StringBuilder();
						result.AppendLine("Dates available for booking in this range:");

                        foreach (var item in availableDates)
                        {
							 result.AppendLine(item.ToString());
                        }
						baseResponse.Description = result.ToString();	

                        baseResponse.Data = true;
					}
					else
					{
						baseResponse.StatusCode = StatusCode.OK;
						baseResponse.Description = "Everything is busy on this range";
						baseResponse.Data = false;
					}
				}
				
			}
			catch (Exception ex)
			{
				return new BaseResponse<bool>()
				{
					Description = $"[AddBooking] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
			return baseResponse;
		}

		public Task<IBaseResponse<bool>> DeleteBooking(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<IBaseResponse<BookingViewModel>> GetBooking(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<IBaseResponse<IEnumerable<BookingViewModel>>> SelectBooking()
		{
			throw new NotImplementedException();
		}

		public Task<IBaseResponse<bool>> UpdateBooking(Guid id, BookingViewModel entity)
		{
			throw new NotImplementedException();
		}
	}
}
