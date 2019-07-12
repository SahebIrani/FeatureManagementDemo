using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

using Sample.FeatureManagement;

[FeatureGate(ConstFeatureFlags.Beta)] // Beta feature flag must be enabled
public class BetaController : Controller
{
	public IActionResult Index() => View();
}