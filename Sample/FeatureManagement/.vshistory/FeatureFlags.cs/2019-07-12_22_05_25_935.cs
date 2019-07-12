namespace Sample.FeatureManagement
{
	// Define your flags in an enum
	// Be careful not to refactor/rename any typos, as that will break configuration
	public enum EnumFeatureFlags
	{
		Home,

		NewWelcomeBanner,

		Beta,

		ChristmasBanner,

		FancyFonts,

		CustomViewData,

		EnhancedPipeline,

		ContentEnhancement,

		NewExperience,
	}

	// Using a static class separates the "name" of the feature flag
	// from its string value
	public static class ConstFeatureFlags
	{
		public const string NewWelcomeBanner = nameof(NewWelcomeBanner);

		public const string Beta = nameof(Beta);

		public const string ChristmasBanner = nameof(ChristmasBanner);

		public const string FancyFonts = nameof(FancyFonts);

		public const string NewExperience = nameof(NewExperience);

		// These flags are safe to access in any context
		public const string NewBranding = "NewBranding";
		public const string AlternativeColours = "AlternativeColours";

		// These flags are only safe to access from an HttpContext-safe request
		public static class Ui
		{
			const string _prefix = "UI_";
			public const string Beta = _prefix + "Beta";
			public const string NewOnboardingExperiences = _prefix + "NewOnboardingExperiences";
		}
	}
}
