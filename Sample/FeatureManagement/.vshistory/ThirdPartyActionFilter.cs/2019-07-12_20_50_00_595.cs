using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Sample.FeatureManagement
{
	public class ThirdPartyActionFilter : IActionFilter
	{
		private ILogger Logger { get; }

		public ThirdPartyActionFilter(ILoggerFactory loggerFactory) => Logger = loggerFactory.CreateLogger<ThirdPartyActionFilter>();

		public void OnActionExecuted(ActionExecutedContext context) => Logger.LogInformation("Third party action filter inward path.");

		public void OnActionExecuting(ActionExecutingContext context) => Logger.LogInformation("Third party action filter outward path.");
	}
}
