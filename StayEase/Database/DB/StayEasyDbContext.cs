using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StayEase.Model.Entity.AccommodationModel;
using StayEase.Model.Entity.Booking;
using StayEase.Model.Entity.User;

namespace StayEase.Database.DB
{
	public class StayEasyDbContext : IdentityDbContext<UserModel, IdentityRole, string>
    {
        public StayEasyDbContext(DbContextOptions<StayEasyDbContext> dbContext) : base(dbContext)
		{
			Database.EnsureCreated();
		}

		public DbSet<AccommodationModel> AccommodationModels { get; set; }
		public DbSet<BookingModel> BookingModels { get; set; }


	}
}
