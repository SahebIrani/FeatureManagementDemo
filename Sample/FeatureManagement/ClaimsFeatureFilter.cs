using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

using System.Linq;

namespace Sample.FeatureManagement
{
	[FilterAlias("Claims")] // How we will refer to the filter in configuration
	public class ClaimsFeatureFilter : IFeatureFilter
	{
		// Used to access HttpContext
		public ClaimsFeatureFilter(IHttpContextAccessor httpContextAccessor) => HttpContextAccessor = httpContextAccessor;

		public IHttpContextAccessor HttpContextAccessor { get; }

		public bool Evaluate(FeatureFilterEvaluationContext context)
		{
			// Get the ClaimsFilterSettings from configuration
			var settings = context.Parameters.Get<ClaimsFilterSettings>();

			// Retrieve the current user (ClaimsPrincipal)
			var user = HttpContextAccessor.HttpContext.User;

			// Only enable the feature if the user has ALL the required claims
			var isEnabled = settings.RequiredClaims
				.All(claimType => user.HasClaim(claim => claim.Type == claimType));

			return isEnabled;
		}
	}
}
