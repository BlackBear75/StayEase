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

		public DbSet<Model.Entity.AccommodationModel.AccommodationModel> AccommodationModels { get; set; }
		public DbSet<Model.Entity.Booking.BookingModel> BookingModels { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<UserModel>(entity =>
			{
				entity.Ignore(u => u.TwoFactorEnabled);
				entity.Ignore(u => u.AccessFailedCount);
				entity.Ignore(u => u.EmailConfirmed);
				entity.Ignore(u => u.PhoneNumberConfirmed);
				entity.Ignore(u => u.LockoutEnabled);
				entity.Ignore(u => u.LockoutEnd);
			});


		}

	}
}
