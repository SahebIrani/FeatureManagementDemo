using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.FeatureManagement;

namespace Simple.Pages
{
	public class IndexModel : PageModel
	{
		public IndexModel(IFeatureManager featureManager) => FeatureManager = featureManager;
		public IFeatureManager FeatureManager { get; }
		public void OnGet()
		{
		}
	}
}
