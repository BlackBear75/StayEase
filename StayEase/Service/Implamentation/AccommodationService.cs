using StayEase.Database.Interface;
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
    }
}
