namespace Simple.FeatureManagement
{
	// Define your flags in an enum
	// Be careful not to refactor/rename any typos, as that will break configuration
	public enum EnumFeatureFlags
	{
		NewWelcomeBanner
	}

	// Using a static class separates the "name" of the feature flag
	// from its string value
	public static class ConstFeatureFlags
	{
		public const string NewWelcomeBanner = nameof(NewWelcomeBanner);
	}
}
