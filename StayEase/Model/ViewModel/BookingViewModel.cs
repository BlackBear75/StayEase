using System.ComponentModel.DataAnnotations;

namespace StayEase.Model.ViewModel
{
	public class BookingViewModel
	{

		private DateTime _startDate;
		private DateTime _endDate;

		[DataType(DataType.Date)] // Вказуємо, що це поле має тип дати
		public DateTime StartDate
		{
			get { return _startDate; }
			set { _startDate = value.Date; } // Встановлюємо дату без годин, хвилин і секунд
		}

		[DataType(DataType.Date)] // Вказуємо, що це поле має тип дати
		public DateTime EndDate
		{
			get { return _endDate; }
			set { _endDate = value.Date; } // Встановлюємо дату без годин, хвилин і секунд
		}






	}
}
