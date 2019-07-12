using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.FeatureManagement;

namespace Sample.Pages
{
	public class IndexModel : PageModel
	{
		public IndexModel(IFeatureManager featureManager)
		{
			FeatureManager = featureManager;

			// only returns true during provided time window
			bool showBanner = FeatureManager.IsEnabled(FeatureManagement.ConstFeatureFlags.ChristmasBanner);

			bool showFancyFonts = FeatureManager.IsEnabled(FeatureManagement.ConstFeatureFlags.FancyFonts);

			var isEnabled = FeatureManager.IsEnabled(FeatureManagement.ConstFeatureFlags.Beta); // may throw!

			// Flags on the main FeatureFlags class are safe to use everywhere
			var isEnabled2 = FeatureManager.IsEnabled(FeatureManagement.ConstFeatureFlags.NewBranding);

			// Flags on the nested Ui class are only safe when HttpContext is available
			var isEnabled3 = FeatureManager.IsEnabled(FeatureManagement.ConstFeatureFlags.Ui.Beta);
		}

		public IFeatureManager FeatureManager { get; }

		public string WelcomeMessage { get; set; }

		public void OnGet()
		{
			// Reference the feature flags using nameof()
			bool isEnabledEnum = FeatureManager.IsEnabled(nameof(FeatureManagement.EnumFeatureFlags.NewWelcomeBanner));

			// No need for nameof() at the call site
			bool isEnabledConst = FeatureManager.IsEnabled(FeatureManagement.ConstFeatureFlags.NewWelcomeBanner);

			WelcomeMessage = FeatureManager.IsEnabled("NewWelcomeBanner")
				? "Welcome to the Beta"
				: "Welcome";

			WelcomeMessage = isEnabledEnum ? "Welcome to the Beta" : "Welcome";

			WelcomeMessage = isEnabledConst ? "Welcome to the Beta" : "Welcome";
		}
	}
}
