using Microsoft.AspNetCore.Mvc;

//[FeatureGate(ConstFeatureFlags.Beta)] // Beta feature flag must be enabled
public class BetaController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}