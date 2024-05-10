using System.ComponentModel.DataAnnotations;
using StayEase.Model.Enum;

namespace StayEase.Model.Entity.AccommodationModel
{
    public class AccommodationModel
    {
		public Guid Id { get; set; }

		[Required]
		public string Address { get; set; }

		[Required]
		public HouseType HouseType { get; set; }
		[Required]
		public int RoomsCount { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public DateTime AvailabilityDate { get; set; }
	}
}
