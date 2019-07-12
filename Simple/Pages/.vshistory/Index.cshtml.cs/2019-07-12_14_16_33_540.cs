using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.FeatureManagement;

using Simple.FeatureManagement;

namespace Simple.Pages
{
	public class IndexModel : PageModel
	{
		public IndexModel(IFeatureManager featureManager) => FeatureManager = featureManager;
		public IFeatureManager FeatureManager { get; }

		public string WelcomeMessage { get; set; }

		public void OnGet()
		{
			// Reference the feature flags using nameof()
			var isEnabled = FeatureManager.IsEnabled(nameof(FeatureFlags.NewWelcomeBanner));

			//WelcomeMessage = FeatureManager.IsEnabled("NewWelcomeBanner")
			//	? "Welcome to the Beta"
			//	: "Welcome";

			WelcomeMessage = isEnabled ? "Welcome to the Beta" : "Welcome";
		}
	}
}
