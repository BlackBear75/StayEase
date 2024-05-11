using System.ComponentModel.DataAnnotations;
using StayEase.Model.Enum;

namespace StayEase.Model.Entity.AccommodationModel
{
    public class AccommodationModel
    {
		public Guid Id { get; set; }

		public Guid UserId { get; set; }


		public string Address { get; set; }
		public string? Description { get; set; }

		public HouseType HouseType { get; set; }
		public int RoomsCount { get; set; }
		
		public decimal Price { get; set; }
	
	}
}
