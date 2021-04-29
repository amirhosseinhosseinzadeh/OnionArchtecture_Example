using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Onion.Web.Infrastructures.Extensions;
using Onion.Services.Security;
using Microsoft.EntityFrameworkCore;
using Onnion.Data;
using Onion.Services.Users;
using Onion.Data.Infrastructures;
using Microsoft.AspNetCore.Http;
using Onion.Web.Infrastructures.Mvc;

namespace Onion.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IRoutePublisher RoutePublisher { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .DefaultCookieBuilder();


            //Service initializing for application object Context
            services.AddDbContext<ObjectContext>(config => config.UseSqlServer(Configuration.GetConnectionString("Default")));

            //Application Services
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IRoutePublisher, RoutePublisher>();
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

            app.UseRouting();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName:"Admin",
                    pattern: "{area=Admin}/{controller=Main}/{action=Index}"
                    );
                this.RoutePublisher = app.ApplicationServices.GetService<IRoutePublisher>();
                RoutePublisher.RegisterRoutes(endpoints);
            });
        }
    }
}
