using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog;
using StayEase.Database.DB;
using StayEase.Model.Entity.User;
using StayEase.Service.Interface;
using StayEase.Service.Implamentation;
using StayEase.Database.Interface;
using StayEase.Database.Repository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<StayEasyDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#region AddScoped
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IAccommodationService, AccommodationService>();
builder.Services.AddScoped<IAccommodationRepository, AccommodationRepository>();

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region logs configure
var logsFolderPath = Path.Combine(builder.Environment.ContentRootPath, "logs");


if (!Directory.Exists(logsFolderPath))
{
	Directory.CreateDirectory(logsFolderPath);
}

Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Debug()
	.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
	.Enrich.FromLogContext()
	.WriteTo.Console()
	.WriteTo.File(new CompactJsonFormatter(), Path.Combine(logsFolderPath, "log.txt")) 
	.CreateLogger();

builder.Host.UseSerilog();
#endregion



builder.Services.AddIdentity<UserModel, IdentityRole>(options =>
{
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 6;
})
	   .AddEntityFrameworkStores<StayEasyDbContext>()
	   .AddDefaultTokenProviders();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var dbContext = services.GetRequiredService<StayEasyDbContext>();

	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserModel>>();
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

	DbInitializer.Initialize(dbContext, userManager, roleManager);
	dbContext.Database.EnsureCreated();



}
app.MapControllers();

app.Run();
