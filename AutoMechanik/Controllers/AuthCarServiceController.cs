using AutoMechanik.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AutoMechanik.Controllers
{
	[Authorize]
	public class AuthCarServiceController : Controller
	{
		private readonly ILogger<AuthCarServiceController> _logger;

		public AuthCarServiceController(ILogger<AuthCarServiceController> logger)
		{
			_logger = logger;
		}

		public IActionResult AuthCarServicePage()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
