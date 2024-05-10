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
		

		}

		private static void InitializeRoles(RoleManager<IdentityRole> roleManager)
		{
		
			string[] roleNames = {  "Client", "Provider" };

			foreach (var roleName in roleNames)
			{
				// Перевірка, чи існує роль
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
	}
}
