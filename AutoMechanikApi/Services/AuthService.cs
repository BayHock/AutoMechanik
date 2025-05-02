using AutoMechanikCore.Models.Dtos.Account;
using AutoMechanikCore.Models.Dtos.Results;
using AutoMechanikCore.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace AutoMechanikApi.Services
{
	public class AuthService
	{
		private readonly UserManager<AutoMechanikUser> _userManager;
		private readonly SignInManager<AutoMechanikUser> _signInManager;
		private readonly LinkGenerator _linkGenerator;
		public string? confirmationUrl;

		public AuthService(
			UserManager<AutoMechanikUser> userManager,
			SignInManager<AutoMechanikUser> signInManager,
			LinkGenerator linkGenerator
			)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_linkGenerator = linkGenerator;
		}

		public async Task<AuthResult> Register(RegisterDto dto)
		{
			if (await _userManager.FindByEmailAsync(dto.Email) != null)
				return AuthResult.Fail("Email уже занят");

			var user = new AutoMechanikUser { Email = dto.Email, UserName = dto.Email };

			var result = await _userManager.CreateAsync(user, dto.Password);

			if (result.Succeeded)
			{
				var roleResult = await _userManager.AddToRoleAsync(user, dto.Role);

				// Не создается ссылка confirmationUrl

				/*if (roleResult.Succeeded)
				{
				var userId = await _userManager.GetUserIdAsync(user);
				var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
				
				confirmationUrl = _linkGenerator 
					.GetUriByAction(
						action: "ConfirmEmail",
						controller: "Account",
						values: new { userId, token },
						scheme: "https",
						host: new HostString("localhost", 7163));

				EmailService.SendEmail(dto.Email, "Confirm your email",
						$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(confirmationUrl)}'>clicking here</a>.");
				}*/

				return AuthResult.Ok();
			}
			else
			{
				var errors = result.Errors.Select(e => e.Description);
				return AuthResult.Fail(errors);
			}
		}

		public async Task<AuthResult> Login(LoginDto dto)
		{
			var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);

			if (result.Succeeded)
			{
				return AuthResult.Ok();
			}
			else if (result.IsLockedOut)
			{
				return AuthResult.Fail("Пользователь заблокирован");
			}
			else
			{
				return AuthResult.Fail("Неверный логин или пароль");
			}
		}
	}
}
