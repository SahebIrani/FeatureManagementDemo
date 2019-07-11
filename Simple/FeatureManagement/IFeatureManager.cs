namespace Simple.FeatureManagement
{
	public interface IFeatureManager
	{
		bool IsEnabled(string feature);
	}
}
