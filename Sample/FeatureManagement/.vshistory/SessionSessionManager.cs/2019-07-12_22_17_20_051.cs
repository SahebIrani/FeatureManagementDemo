using Microsoft.AspNetCore.Http;
using Microsoft.FeatureManagement;

namespace Sample.FeatureManagement
{
	public class SessionSessionManager : ISessionManager
	{
		public SessionSessionManager(IHttpContextAccessor contextAccessor) => ContextAccessor = contextAccessor;
		public IHttpContextAccessor ContextAccessor { get; }

		public void Set(string featureName, bool enabled)
		{
			var session = _contextAccessor.HttpContext.Session;
			var sessionKey = $"feature_{featureName}";
			session.Set(sessionKey, new[] { enabled ? (byte)1 : (byte)0 });
		}

		public bool TryGet(string featureName, out bool enabled)
		{
			var session = ContextAccessor.HttpContext.Session;
			if (session.TryGetValue($"feature_{featureName}", out var enabledBytes))
			{
				enabled = enabledBytes[0] == 1;
				return true;
			}

			enabled = false;
			return false;
		}
	}
}