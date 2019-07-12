using System.Collections.Generic;

namespace Sample.FeatureManagement
{
	public class BrowserFilterSettings
	{
		public IList<string> AllowedBrowsers { get; set; } = new List<string>();
	}
}
