using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;

using Sample.Data;
using Sample.FeatureManagement;

namespace Sample
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			ConfigurationBuilder builder = new ConfigurationBuilder();

			builder.AddJsonFile("appsettings.json", false, true);

			Configuration = builder.Build();

			Configuration = configuration; //◘◘XOKX◘◘
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

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddDefaultIdentity<IdentityUser>()
				.AddDefaultUI(UIFramework.Bootstrap4)
				.AddEntityFrameworkStores<ApplicationDbContext>();

			//services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			// Add IHttpContextAccessor if it's not yet added
			services.AddSession();
			services.AddHttpContextAccessor();
			services.AddTransient<ISessionManager, SessionSessionManager>();

			services.AddFeatureManagement()
				.AddFeatureFilter<ClaimsFeatureFilter>()
				.AddFeatureFilter<BrowserFilter>()
				.AddFeatureFilter<TimeWindowFilter>()
				.AddFeatureFilter<PercentageFilter>()
				.AddSessionManager<SessionSessionManager>()
				.UseDisabledFeaturesHandler(new RedirectDisabledFeatureHandler())
				.UseDisabledFeaturesHandler(new FeatureNotEnabledDisabledHandler())
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

			app.UseAuthentication();
			app.UseAuthorization();
			app.UseSession();

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
