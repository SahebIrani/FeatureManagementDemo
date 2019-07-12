using Microsoft.FeatureManagement;

namespace Sample.FeatureManagement
{
	public class SuperUserFilter : IFeatureFilter
	{
		public bool Evaluate(FeatureFilterEvaluationContext context)
		{
			return false;
		}
	}
}
