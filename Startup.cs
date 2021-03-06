using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManager.Authorization;
using TaskManager.Data;
using TaskManager.Services;
using WebPWrecover.Services;

namespace TaskManager
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));
			services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "wwwroot/ClientApp/build2";
			});

			services.AddControllersWithViews();
			// requires
			// using Microsoft.AspNetCore.Identity.UI.Services;
			// using WebPWrecover.Services;
			services.AddTransient<IEmailSender, EmailSender>();
			services.Configure<AuthMessageSenderOptions>(Configuration);

			services.AddRazorPages();
			
			services.AddControllers(config =>
			{
				// using Microsoft.AspNetCore.Mvc.Authorization;
				// using Microsoft.AspNetCore.Authorization;
				var policy = new AuthorizationPolicyBuilder()
								 .RequireAuthenticatedUser()
								 .Build();
				config.Filters.Add(new AuthorizeFilter(policy));
			});
			// Authorization handlers.
			services.AddScoped<IAuthorizationHandler,
								  OwnerAuthorizationHandler>();

			services.AddScoped<IAuthorizationHandler,
								  OwnerAHTasking>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseCors(options =>
			options.WithOrigins("https://localhost:44315")
			.AllowAnyHeader()
			.AllowAnyMethod());

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				//app.UseExceptionHandler("/Error");
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "wwwroot/ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseReactDevelopmentServer(npmScript: "start");
				}
			});
		}
	}
}
