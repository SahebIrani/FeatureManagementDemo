using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.FeatureManagement;

namespace Sample.Pages
{
	public class IndexModel : PageModel
	{
		public IndexModel(IFeatureManager featureManager) => FeatureManager = featureManager;
		public IFeatureManager FeatureManager { get; }

		public string WelcomeMessage { get; set; }

		public void OnGet()
		{
			// Reference the feature flags using nameof()
			var isEnabledEnum = FeatureManager.IsEnabled(nameof(FeatureManagement.EnumFeatureFlags.NewWelcomeBanner));

			// No need for nameof() at the call site
			var isEnabledConst = FeatureManager.IsEnabled(FeatureManagement.ConstFeatureFlags.NewWelcomeBanner);

			//WelcomeMessage = FeatureManager.IsEnabled("NewWelcomeBanner")
			//	? "Welcome to the Beta"
			//	: "Welcome";

			WelcomeMessage = isEnabled ? "Welcome to the Beta" : "Welcome";
		}
	}
}
