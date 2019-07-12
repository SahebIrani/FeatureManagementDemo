using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;

using Sample.Models;

using System.Diagnostics;

namespace Sample.Controllers
{
	public class HomeController : Controller
	{
		public IFeatureManager FeatureManager { get; }

		public HomeController(IFeatureManagerSnapshot featureSnapshot) => FeatureManager = featureSnapshot;

		public IActionResult Privacy()
		{
			ViewData["Message"] = "Your application description page.";

			if (FeatureManager.IsEnabled(nameof(Sample.FeatureManagement.EnumFeatureFlags.CustomViewData)))
			{
				ViewData["Message"] = $"This is FANCY CONTENT you can see only if '{nameof(Sample.FeatureManagement.EnumFeatureFlags.CustomViewData)}' is enabled.";
			};

			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
