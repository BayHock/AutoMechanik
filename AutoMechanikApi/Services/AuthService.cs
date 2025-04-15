using AutoMechanikCore.Models.Dtos.Account;
using AutoMechanikCore.Models.Dtos.Results;
using AutoMechanikCore.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace AutoMechanikApi.Services
{
	public class AuthService
	{
		private readonly UserManager<AutoMechanikUser> _userManager;

		public AuthService(
			UserManager<AutoMechanikUser> userManager
			)
		{
			_userManager = userManager;
		}

		public async Task<AuthResult> Register(RegisterDto dto)
		{
			if (await _userManager.FindByEmailAsync(dto.Email) != null)
				return AuthResult.Fail("Email уже занят");

			var user = new AutoMechanikUser { Email = dto.Email, UserName = dto.Email };

			var result = await _userManager.CreateAsync(user, dto.Password);

			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description);
				return AuthResult.Fail(errors);
			}

			return AuthResult.Ok();

			//if (result.Succeeded)
			//{
			//	var roleResult = await _userManager.AddToRoleAsync(user, dto.Role);

			//	if (roleResult.Succeeded)
			//	{

			//		var userId = await _userManager.GetUserIdAsync(user);
			//		var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

			//		code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

			//		var callbackUrl = $"https://localhost:5001/Account/ConfirmEmail?userId={userId}&code={code}"; //Добавить переход на страницу ConfirmEmail

			//		EmailService.SendEmail(
			//			dto.Email,
			//			"Confirm your email",
			//			$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
			//	}
			//}

		}
	}
}
