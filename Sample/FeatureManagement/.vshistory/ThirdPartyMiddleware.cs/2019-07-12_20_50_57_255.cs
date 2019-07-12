using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace Sample.FeatureManagement
{
	public class ThirdPartyMiddleware
	{
		//
		// The middleware delegate to call after this one finishes processing
		private readonly RequestDelegate _next;
		private ILogger Logger { get; }

		public ThirdPartyMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
		{
			_next = next;
			Logger = loggerFactory.CreateLogger<ThirdPartyMiddleware>();
		}

		public async Task Invoke(HttpContext httpContext)
		{
			Logger.LogInformation($"Third party middleware inward path.");

			//
			// Call the next middleware delegate in the pipeline
			await _next.Invoke(httpContext);

			Logger.LogInformation($"Third party middleware outward path.");
		}
	}
}
