// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable
using AutoMechanikCore.Services;
using AutoMechanikCore.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

namespace AutoMechanik.Areas.Identity.Pages.Account
{
	public class RegisterModel : PageModel
	{
		private readonly SignInManager<AutoMechanikUser> _signInManager;
		private readonly UserManager<AutoMechanikUser> _userManager;
		private readonly IUserStore<AutoMechanikUser> _userStore;
		private readonly IUserEmailStore<AutoMechanikUser> _emailStore;
		private readonly ILogger<RegisterModel> _logger;

		public RegisterModel(
			UserManager<AutoMechanikUser> userManager,
			IUserStore<AutoMechanikUser> userStore,
			SignInManager<AutoMechanikUser> signInManager,
			ILogger<RegisterModel> logger)
		{
			_userManager = userManager;
			_userStore = userStore;
			_emailStore = GetEmailStore();
			_signInManager = signInManager;
			_logger = logger;
		}

		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		[BindProperty]
		public InputModel Input { get; set; }

		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		public string ReturnUrl { get; set; }

		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		public IList<AuthenticationScheme> ExternalLogins { get; set; }

		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		
		public EmailService EmailService { get; set; }

		public class InputModel
		{
			[Required]
			[EmailAddress]
			[Display(Name = "Email")]
			public string Email { get; set; }

			[Required]
			[DataType(DataType.Password)]
			[Display(Name = "Password")]
			public string Password { get; set; }

			[Required]
			public string Role { get; set; }
		}


		public async Task OnGetAsync(string returnUrl = null)
		{
			if (User.Identity.IsAuthenticated)
			{
				var user = await _userManager.GetUserAsync(User);
				var roles = await _userManager.GetRolesAsync(user);

				if (roles.Contains("Client"))
				{
					Response.Redirect("/HomeUser/HomeUserPage");
				}
				else if (roles.Contains("AutoService"))
				{
					Response.Redirect("/HomeAutoService/HomeAutoServicePage");
				}
				else
				{
					Response.Redirect("/Home/Index");
				}
			}
			ReturnUrl = returnUrl;
			ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
		}

		public async Task<IActionResult> OnPostAsync(string returnUrl = null)
		{
			returnUrl ??= Url.Content("~/");
			ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
			if (ModelState.IsValid)
			{
				var user = CreateUser();

				await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
				await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
				var result = await _userManager.CreateAsync(user, Input.Password);

				if (result.Succeeded)
				{
					_logger.LogInformation("User created a new account with password.");

					var roleResult = await _userManager.AddToRoleAsync(user, Input.Role);

					if (roleResult.Succeeded)
					{
						var userId = await _userManager.GetUserIdAsync(user);
						var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
						code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
						var callbackUrl = Url.Page(
							"/Account/ConfirmEmail",
							pageHandler: null,
							values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
							protocol: Request.Scheme);

						//Метод отправки Email локально
						EmailService.SendEmail(Input.Email, "Confirm your email",
							$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

						if (_userManager.Options.SignIn.RequireConfirmedAccount)
						{
							return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
						}
						else
						{
							await _signInManager.SignInAsync(user, isPersistent: false);

							if (Input.Role == "Client")
							{
								return RedirectToAction("HomeUserPage", "HomeUser");
							}
							else if (Input.Role == "AutoService")
							{
								return RedirectToAction("HomeAutoServicePage", "HomeAutoService");
							}

						}
					}
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			// If we got this far, something failed, redisplay form
			return Page();
		}

		private AutoMechanikUser CreateUser()
		{
			try
			{
				return Activator.CreateInstance<AutoMechanikUser>();
			}
			catch
			{
				throw new InvalidOperationException($"Can't create an instance of '{nameof(AutoMechanikUser)}'. " +
					$"Ensure that '{nameof(AutoMechanikUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
					$"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
			}
		}

		private IUserEmailStore<AutoMechanikUser> GetEmailStore()
		{
			if (!_userManager.SupportsUserEmail)
			{
				throw new NotSupportedException("The default UI requires a user store with email support.");
			}
			return (IUserEmailStore<AutoMechanikUser>)_userStore;
		}
	}
}
