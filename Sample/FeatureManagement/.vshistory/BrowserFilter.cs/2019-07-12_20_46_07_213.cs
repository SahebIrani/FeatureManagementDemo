using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

using System;
using System.Linq;

namespace Sample.FeatureManagement
{
	[FilterAlias("Browser")]
	public class BrowserFilter : IFeatureFilter
	{
		private const string Chrome = "Chrome";
		private const string Edge = "Edge";

		public BrowserFilter(IHttpContextAccessor httpContextAccessor) =>
			HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

		private IHttpContextAccessor HttpContextAccessor { get; }

		public bool Evaluate(FeatureFilterEvaluationContext context)
		{
			BrowserFilterSettings settings = context.Parameters.Get<BrowserFilterSettings>() ?? new BrowserFilterSettings();

			if (settings.AllowedBrowsers.Any(browser => browser.Equals(Chrome, StringComparison.OrdinalIgnoreCase)) && IsChrome())
			{
				return true;
			}
			else if (settings.AllowedBrowsers.Any(browser => browser.Equals(Edge, StringComparison.OrdinalIgnoreCase)) && IsEdge())
			{
				return true;
			}

			return false;
		}

		private bool IsChrome()
		{
			string userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"];

			return userAgent != null && userAgent.Contains("Chrome", StringComparison.OrdinalIgnoreCase) && !userAgent.Contains("edge", StringComparison.OrdinalIgnoreCase);
		}

		private bool IsEdge()
		{
			// Return true if current request is sent from Edge browser

			return false;
		}
	}
}
