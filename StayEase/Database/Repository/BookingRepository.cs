using Microsoft.EntityFrameworkCore;
using StayEase.Database.DB;
using StayEase.Database.Interface;
using StayEase.Model.Entity.Booking;
using StayEase.Model.ViewModel;


namespace StayEase.Database.Repository
{
	public class BookingRepository : IBookingRepository
	{
        private readonly StayEasyDbContext _db;
        public BookingRepository(StayEasyDbContext db)
        {
            _db = db;
        }

		public async Task<bool> AddBooking(BookingModel entity)
		{
			try
			{
				_db.BookingModels.Add(entity);

				await _db.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<bool> DeleteBooking(BookingModel entity)
		{
			try
			{
				_db.BookingModels.Remove(entity);

				await _db.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<BookingModel> GetBooking(Guid id)
		{
			return await _db.BookingModels.FirstOrDefaultAsync(x => x.Id == id);
		}

		
		public async Task<IEnumerable<BookingModel>> SelectBooking()
		{
			return await _db.BookingModels.ToListAsync();
		}

		public async Task<bool> UpdateBooking(BookingModel entity)
		{
			try
			{
				_db.BookingModels.Update(entity);
				await _db.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public async Task<bool> IsAccommodationAvailable(Guid accommodationId, BookingViewModel bookingView)
		{
			var bookings = await _db.BookingModels.Where(b => b.AccommodationId == accommodationId).ToListAsync();

			// Перевіряємо кожне бронювання
			foreach (var booking in bookings)
			{ 
				if (!(bookingView.EndDate <= booking.StartDate || bookingView.StartDate >= booking.EndDate))
				{
					return false;
				}
			}

			return true;
		}
		public async Task<List<DateTime>> GetAvailableDates(Guid accommodationId, BookingViewModel bookingView)
		{
			// Отримуємо всі бронювання для обраного місця
			var bookings = await _db.BookingModels.Where(b => b.AccommodationId == accommodationId).ToListAsync();

			// Створюємо список вільних дат
			List<DateTime> availableDates = new List<DateTime>();

			// Проходимо через кожен день у заданому діапазоні
			for (DateTime date = bookingView.StartDate; date <= bookingView.EndDate; date = date.AddDays(1))
			{
				// Перевіряємо, чи є ця дата доступною для бронювання
				bool isAvailable = true;

				foreach (var booking in bookings)
				{
					// Якщо дата перекривається з будь-яким існуючим бронюванням, вона не є доступною
					if (date >= booking.StartDate && date <= booking.EndDate)
					{
						isAvailable = false;
						break;
					}
				}

				// Якщо дата доступна, додаємо її до списку вільних дат
				if (isAvailable)
				{
					availableDates.Add(date);
				}
			}

			return availableDates;
		}


	}
}
