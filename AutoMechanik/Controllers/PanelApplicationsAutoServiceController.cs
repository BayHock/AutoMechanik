using AutoMechanikCore.Models;
using AutoMechanikCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AutoMechanikMVC.Controllers
{
	[Authorize(Roles = "AutoService")]
	public class PanelApplicationsAutoServiceController : Controller
	{
		private readonly ApiApplicationService _applicationService;
		private readonly ILogger<PanelApplicationsAutoServiceController> _logger;

		public PanelApplicationsAutoServiceController (ILogger<PanelApplicationsAutoServiceController> logger, ApiApplicationService applicationService)
		{
			_applicationService = applicationService;
			_logger = logger;

		}
		public async Task<IActionResult> Index()
		{
			var applications = await _applicationService.GetApplicationsAsync();
			return View(applications);
		}

		[HttpPost]
		public async Task<IActionResult> Create(ApplicationModel application)
		{
			await _applicationService.CreateApplicationAsync(application);
			return RedirectToAction("PanelApplicationsAutoServicePage");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
