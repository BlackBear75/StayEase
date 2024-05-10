namespace StayEase.Model.Entity.Booking
{
    public class BookingModel
    {
		public Guid Id { get; set; }

		public Guid UserId { get; set; }

		public Guid AccommodationId { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

	}
}
