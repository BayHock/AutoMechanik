using AutoMechanik.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace AutoMechanik.Services
{
	public static class SeedData
	{
		public static async Task Initialize(IServiceProvider serviceProvider)
		{
			var roleManager = serviceProvider.GetRequiredService<RoleManager<AutoMechanikRole>>();

			string[] roleNames = { "AutoService", "Client" };

			foreach (var roleName in roleNames)
			{
				var roleExist = await roleManager.RoleExistsAsync(roleName);
				if (!roleExist)
				{
					await roleManager.CreateAsync(new AutoMechanikRole { Name = roleName });
				}
			}
		}
	}
}
