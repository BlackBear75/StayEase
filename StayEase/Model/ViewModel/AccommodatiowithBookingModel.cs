using StayEase.Model.Entity.AccommodationModel;

namespace StayEase.Model.ViewModel
{
	public class AccommodatiowithBookingModel
	{
		public AccommodatiowithBookingModel()
		{
			Accomodation = new AccomodationViewModel();
		Booking = new	BookingViewModel();
		}

		public AccomodationViewModel Accomodation { get; set; }
		public BookingViewModel Booking { get; set; }
	}
}
