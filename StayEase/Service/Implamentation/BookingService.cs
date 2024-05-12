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
		private readonly IAccommodationRepository _accommodationRepository;
		public BookingService(IBookingRepository bookingRepository, IAccommodationRepository accommodationRepository)
        {
            _bookingRepository = bookingRepository;
			_accommodationRepository = accommodationRepository;
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

		public async Task<IBaseResponse<bool>> DeleteBooking(Guid id)
		{
			var baseResponse = new BaseResponse<bool>();
			try
			{
				var booking = await _bookingRepository.GetBooking(id);
				if (booking == null)
				{
					baseResponse.StatusCode = StatusCode.EntityNull;
					baseResponse.Description = "deletebooking = null entity";
					baseResponse.Data = false;
					return baseResponse;
				}


				await _bookingRepository.DeleteBooking(booking);


				baseResponse.StatusCode = StatusCode.OK;
				baseResponse.Description = "Good delete booking";
				baseResponse.Data = true;
			}
			catch (Exception ex)
			{
				return new BaseResponse<bool>()
				{
					Description = $"[DeleteBooking] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
			return baseResponse;
		}

		public async Task<IBaseResponse<AccommodatiowithBookingModel>> GetBooking(Guid id)
		{
			var baseResponse = new BaseResponse<AccommodatiowithBookingModel>();
			try
			{
				var booking = await _bookingRepository.GetBooking(id);
				if (booking == null)
				{
					baseResponse.StatusCode = StatusCode.EntityNull;
					baseResponse.Description = "GetBooking = null entity";
					return baseResponse;
				}
			
				AccommodatiowithBookingModel model = new AccommodatiowithBookingModel();
			
				var accomodation = await _accommodationRepository.GetAccommodation(booking.AccommodationId);

				model.Accomodation.Address = accomodation.Address;
				model.Accomodation.Price = accomodation.Price;
				model.Accomodation.RoomsCount = accomodation.RoomsCount;
				model.Accomodation.Description = accomodation.Description;
				model.Accomodation.HouseType = accomodation.HouseType;
				model.Booking.StartDate = booking.StartDate;
				model.Booking.EndDate = booking.EndDate;


				baseResponse.StatusCode = StatusCode.OK;
				baseResponse.Description = "GetBooking succesful";
				baseResponse.Data = model;
			}
			catch (Exception ex)
			{
				return new BaseResponse<AccommodatiowithBookingModel>()
				{
					Description = $"[GetBooking] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
			return baseResponse;
		}

		public async Task<IBaseResponse<IEnumerable<AccommodatiowithBookingModel>>> SelectBooking()
		{
			var baseResponse = new BaseResponse<IEnumerable<AccommodatiowithBookingModel>>();
			try
			{
				var bookings = await _bookingRepository.SelectBooking();
				if (bookings == null)
				{
					baseResponse.StatusCode = StatusCode.EntityNull;
					baseResponse.Description = "SelectBooking no find bookings";
					return baseResponse;
				}
				List<AccommodatiowithBookingModel> accommodatiowithBookings = new List<AccommodatiowithBookingModel>();
				AccommodatiowithBookingModel model = new AccommodatiowithBookingModel();
				foreach (var booking in bookings)
				{
					var accomodation = await _accommodationRepository.GetAccommodation(booking.AccommodationId);

					model.Accomodation.Address = accomodation.Address;
					model.Accomodation.Price = accomodation.Price;
					model.Accomodation.RoomsCount = accomodation.RoomsCount;
					model.Accomodation.Description = accomodation.Description;
					model.Accomodation.HouseType = accomodation.HouseType;
					model.Booking.StartDate = booking.StartDate;
					model.Booking.EndDate = booking.EndDate;

					accommodatiowithBookings.Add(model);
				}

				baseResponse.StatusCode = StatusCode.OK;
				baseResponse.Description = "SelectBooking succesful";
				baseResponse.Data = accommodatiowithBookings;
			}
			catch (Exception ex)
			{
				return new BaseResponse<IEnumerable<AccommodatiowithBookingModel>>()
				{
					Description = $"[SelectBooking] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
			return baseResponse;
		}

		public async Task<IBaseResponse<IEnumerable<AccommodatiowithBookingModel>>> SelectUserBooking(Guid id)
		{
			var baseResponse = new BaseResponse<IEnumerable<AccommodatiowithBookingModel>>();
			try
			{
				var bookings = await _bookingRepository.SelectUserBooking(id);
				if (bookings == null)
				{
					baseResponse.StatusCode = StatusCode.EntityNull;
					baseResponse.Description = "SelectUserBooking no find bookings";
					return baseResponse;
				}


				List<AccommodatiowithBookingModel> accommodatiowithBookings = new List<AccommodatiowithBookingModel>();
				AccommodatiowithBookingModel model = new AccommodatiowithBookingModel();
				foreach (var booking in bookings)
				{
					var accomodation = await _accommodationRepository.GetAccommodation(booking.AccommodationId);

					model.Accomodation.Address = accomodation.Address;
					model.Accomodation.Price = accomodation.Price;
					model.Accomodation.RoomsCount = accomodation.RoomsCount;
					model.Accomodation.Description = accomodation.Description;
					model.Accomodation.HouseType = accomodation.HouseType;
					model.Booking.StartDate = booking.StartDate;
					model.Booking.EndDate = booking.EndDate;

					accommodatiowithBookings.Add(model);
				}

				baseResponse.StatusCode = StatusCode.OK;
				baseResponse.Description = "SelectUserBooking succesful";
				baseResponse.Data = accommodatiowithBookings;


			}
			catch (Exception ex)
			{
				return new BaseResponse<IEnumerable<AccommodatiowithBookingModel>>()
				{
					Description = $"[SelectUserBooking] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
			return baseResponse;
		}
	}
}
