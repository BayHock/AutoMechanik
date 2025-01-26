using AutoMechanik.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AutoMechanik.Controllers
{
	[Authorize]
	public class HomeAutoServiceController : Controller
	{
		private readonly ILogger<HomeAutoServiceController> _logger;

		public HomeAutoServiceController(ILogger<HomeAutoServiceController> logger)
		{
			_logger = logger;
		}
		public IActionResult HomeAutoServicePage()
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
