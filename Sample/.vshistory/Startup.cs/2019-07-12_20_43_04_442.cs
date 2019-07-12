using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;

using Sample.FeatureManagement;

namespace Sample
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			var builder = new ConfigurationBuilder();

			builder.AddJsonFile("appsettings.json", false, true);

			Configuration = builder.Build();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
			});

			services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddHttpContextAccessor();

			services.AddFeatureManagement()
				//.AddFeatureFilter<BrowserFilter>()
				.AddFeatureFilter<TimeWindowFilter>()
				.AddFeatureFilter<PercentageFilter>()
				.UseDisabledFeaturesHandler(new RedirectDisabledFeatureHandler())
			//.UseDisabledFeaturesHandler(new FeatureNotEnabledDisabledHandler())
			;

			services.AddControllersWithViews();
			services.AddRazorPages();

			//services.AddMvc(o =>
			//{
			//	o.Filters.AddForFeature<ThirdPartyActionFilter>(nameof(MyFeatureFlags.EnhancedPipeline));

			//}).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseCookiePolicy();

			app.UseRouting();

			app.UseAuthorization();

			//app.UseMiddlewareForFeature<ThirdPartyMiddleware>(nameof(MyFeatureFlags.EnhancedPipeline));

			app.UseEndpoints(endpoints =>
			{
				// Use a route that requires a feature to be enabled
				//endpoints.MapRouteForFeature("Beta", "betaDefault", "{controller=Beta}/{action=Index}/{id?}", null, null, null);

				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");

				endpoints.MapRazorPages();
			});
		}
	}
}
