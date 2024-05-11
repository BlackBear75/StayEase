using StayEase.Model.Entity.BaseResponce;
using StayEase.Model.ViewModel;

namespace StayEase.Service.Interface
{
	public interface IAccommodationService
	{
		Task<IBaseResponse<bool>> AddAccommodation(AccomodationViewModel entity,Guid userid);

		Task<IBaseResponse<AccomodationViewModel>> GetAccommodation(Guid id);
		Task<IBaseResponse<IEnumerable<AccomodationViewModel>>> SelectAccommodation();

		Task<IBaseResponse<bool>> DeleteAccommodation(Guid id);
		Task<IBaseResponse<bool>> UpdateAccommodation(Guid id,AccomodationViewModel entity);






	}
}
