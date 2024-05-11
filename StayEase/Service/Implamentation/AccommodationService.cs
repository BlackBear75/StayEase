using Microsoft.AspNetCore.Identity;
using StayEase.Database.Interface;
using StayEase.Model.Entity.AccommodationModel;
using StayEase.Model.Entity.BaseResponce;
using StayEase.Model.Entity.User;
using StayEase.Model.Enum;
using StayEase.Model.ViewModel;
using StayEase.Service.Interface;

namespace StayEase.Service.Implamentation
{
	public class AccommodationService : IAccommodationService
	{
        private readonly IAccommodationRepository _accommodationRepository;
        public AccommodationService(IAccommodationRepository accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
        }

		public async Task<IBaseResponse<bool>> AddAccommodation(AccomodationViewModel entity, Guid userid)
		{
			var baseResponse = new BaseResponse<bool>();
			try
			{
				if (entity == null)
				{
					baseResponse.StatusCode = StatusCode.EntityNull;
					baseResponse.Description = "AddAccommodation = null";
					baseResponse.Data = false;
					return baseResponse;
				}

				var model = new AccommodationModel();
				model.Id = Guid.NewGuid();
				model.Price = entity.Price;
				model.Address = entity.Address;
				model.Description = entity.Description;
				model.HouseType = entity.HouseType;
				model.UserId = userid;


				await _accommodationRepository.AddAccommodation(model);


				baseResponse.StatusCode = StatusCode.OK;
				baseResponse.Description = "AddAccommodation succesful";
				baseResponse.Data = true;
			}
			catch (Exception ex)
			{
				return new BaseResponse<bool>()
				{
					Description = $"[AddAccommodation] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
			return baseResponse;
		}

		public async Task<IBaseResponse<bool>> DeleteAccommodation(Guid id)
		{
			var baseResponse = new BaseResponse<bool>();
			try
			{
				var accommodation = await _accommodationRepository.GetAccommodation(id);
				if (accommodation == null)
				{
					baseResponse.StatusCode = StatusCode.EntityNull;
					baseResponse.Description = "DeleteAccommodation = null entity";
					baseResponse.Data = false;
					return baseResponse;
				}


				await _accommodationRepository.DeleteAccommodation(accommodation);


				baseResponse.StatusCode = StatusCode.OK;
				baseResponse.Description = "DeleteAccommodation succesful";
				baseResponse.Data = true;
			}
			catch (Exception ex)
			{
				return new BaseResponse<bool>()
				{
					Description = $"[DeleteAccommodation] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
			return baseResponse;
		}

		public async Task<IBaseResponse<AccomodationViewModel>> GetAccommodation(Guid id)
		{
			var baseResponse = new BaseResponse<AccomodationViewModel>();
			try
			{
				var accommodation = await _accommodationRepository.GetAccommodation(id);
				if (accommodation == null)
				{
					baseResponse.StatusCode = StatusCode.EntityNull;
					baseResponse.Description = "GetAccommodation = null entity";

					return baseResponse;
				}

				var viewproduct = new AccomodationViewModel
				{
				
					Address = accommodation.Address,
					HouseType = accommodation.HouseType,
					Description = accommodation.Description,
					Price = accommodation.Price,
					RoomsCount = accommodation.RoomsCount,
				};



				baseResponse.StatusCode = StatusCode.OK;
				baseResponse.Description = "GetAccommodation succesful";
				baseResponse.Data = viewproduct;
			}
			catch (Exception ex)
			{
				return new BaseResponse<AccomodationViewModel>()
				{
					Description = $"[GetAccommodation] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
			return baseResponse;
		}

		public async Task<IBaseResponse<IEnumerable<AccomodationViewModel>>> SelectAccommodation()
		{
			var baseResponse = new BaseResponse<IEnumerable<AccomodationViewModel>>();
			try
			{
				var accommodations = await _accommodationRepository.SelectAccommodation();
				if (accommodations == null)
				{
					baseResponse.StatusCode = StatusCode.EntityNull;
					baseResponse.Description = "SelectAccommodation no find accommodation";
					
					return baseResponse;
				}


				List<AccomodationViewModel> viewaccommodation = new List<AccomodationViewModel>();
				AccomodationViewModel model = new AccomodationViewModel();
				foreach (var item in accommodations)
				{
					
					model.Address = item.Address;
					model.Description = item.Description;
					model.Price = item.Price;
					model.RoomsCount = item.RoomsCount;
					model.HouseType = item.HouseType;
					viewaccommodation.Add(model);
				}

				baseResponse.StatusCode = StatusCode.OK;
				baseResponse.Description = "SelectAccommodation succesful";
				baseResponse.Data = viewaccommodation;
			}
			catch (Exception ex)
			{
				return new BaseResponse<IEnumerable<AccomodationViewModel>>()
				{
					Description = $"[SelectAccommodation] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
			return baseResponse;
		}

		public async Task<IBaseResponse<bool>> UpdateAccommodation(Guid id, AccomodationViewModel entity)
		{
			var baseResponse = new BaseResponse<bool>();
			try
			{
				var accommodation = await _accommodationRepository.GetAccommodation(id);
				if (accommodation == null)
				{
					baseResponse.StatusCode = StatusCode.EntityNull;
					baseResponse.Description = "Updade accommodation no entity";
					baseResponse.Data = false;
					return baseResponse;
				}
				
				accommodation.Price = entity.Price;
				accommodation.Address = entity.Address;
				accommodation.HouseType = entity.HouseType;
				accommodation.RoomsCount = entity.RoomsCount;

				


				await _accommodationRepository.UpdateAccommodation(accommodation);
				baseResponse.StatusCode = StatusCode.OK;
				baseResponse.Description = "UpdateAccommodation succesful";

				baseResponse.Data = true;
			}
			catch (Exception ex)
			{
				return new BaseResponse<bool>()
				{
					Description = $"[UpdateAccommodation] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
			return baseResponse;
		}
	}
}
