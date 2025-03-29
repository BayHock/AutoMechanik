using AutoMechanikCore.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AutoMechanikCore.Services
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
