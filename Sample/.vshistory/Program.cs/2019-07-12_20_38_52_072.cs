using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using System.Linq;

namespace Sample
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.ConfigureAppConfiguration((ctx, builder) =>
					{
						var settings = builder.Build();

						if (!string.IsNullOrEmpty(settings["AppConfiguration:ConnectionString"]))
						{
							//
							// This section can be used to pull feature flag configuration from Azure App Configuration
							builder.AddAzureAppConfiguration(o =>
							{
								o.Connect(settings["AppConfiguration:ConnectionString"]);

								o.Use(KeyFilter.Any);

								o.UseFeatureFlags();
							});
						}
					})
					.UseStartup<Startup>();
				});
	}
}
