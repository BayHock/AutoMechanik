using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoMechanik.Controllers
{
	[Authorize]
	public class BaseController : Controller
	{
		public IActionResult Main()
		{
			return View();
		}
	}
}
