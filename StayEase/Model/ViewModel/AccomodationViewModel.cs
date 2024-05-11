using StayEase.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace StayEase.Model.ViewModel
{
	public class AccomodationViewModel
	{
	

		public string Address { get; set; }

		public HouseType HouseType { get; set; }

		public string? Description { get; set; }
		public int RoomsCount { get; set; }
	
		public decimal Price { get; set; }
		
	
	}
}
