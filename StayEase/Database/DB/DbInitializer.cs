using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StayEase.Model.Entity.User;

namespace StayEase.Database.DB
{
	public class DbInitializer
	{
		public static void Initialize(StayEasyDbContext context, UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager)
		{
			// Виконати міграцію бази даних
			context.Database.Migrate();

			// Ініціалізувати ролі
			InitializeRoles(roleManager);

			InitializeUsers(userManager);
		}

		private static void InitializeRoles(RoleManager<IdentityRole> roleManager)
		{
			// Додайте ваші ролі, якщо потрібно
			string[] roleNames = { "Admin", "User" };

			foreach (var roleName in roleNames)
			{
				var roleExists = roleManager.RoleExistsAsync(roleName).Result;

				if (!roleExists)
				{
					var role = new IdentityRole
					{
						Name = roleName
					};
					var result = roleManager.CreateAsync(role).Result;
				}
			}
		}

		private static void InitializeUsers(UserManager<UserModel> userManager)
		{
			// Додайте вашого адміністратора, якщо потрібно
			var adminUser = new UserModel
			{
				UserName = "Admin_1",
				Email = "admin@example.com"
			};

			var adminUserExists = userManager.FindByEmailAsync(adminUser.Email).Result;

			if (adminUserExists == null)
			{
				var result = userManager.CreateAsync(adminUser, "Admin1!").Result;
				if (result.Succeeded)
				{
					userManager.AddToRoleAsync(adminUser, "Admin").Wait();
				}
			}
		}
	}
}

