using StayEase.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace StayEase.Model.ViewModel
{
	public class AccomodationViewModel
	{


		[Required(ErrorMessage = "Address is required")]
		public string Address { get; set; }

		[Required(ErrorMessage = "HouseType is required")]
		public HouseType HouseType { get; set; }

		[StringLength(500, ErrorMessage = "Description length can't be more than 500 characters")]
		public string Description { get; set; }

		[Required(ErrorMessage = "RoomsCount is required")]
		[Range(1, int.MaxValue, ErrorMessage = "RoomsCount must be greater than 0")]
		public int RoomsCount { get; set; }

		[Required(ErrorMessage = "Price is required")]
		[Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
		public decimal Price { get; set; }


	}
}
