using StayEase.Model.Entity.AccommodationModel;
using StayEase.Model.Entity.BaseResponce;
using StayEase.Model.ViewModel;

namespace StayEase.Database.Interface
{
	public interface IAccommodationRepository
	{
		Task<bool> AddAccommodation(AccommodationModel entity);

		Task<AccommodationModel> GetAccommodation(Guid id);
		Task<IEnumerable<AccommodationModel>> SelectAccommodation();

		Task<bool> DeleteAccommodation(AccommodationModel entity);
		Task<bool> UpdateAccommodation(AccommodationModel entity);

	}
}
