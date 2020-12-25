using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pi_ETicaret_Sitesi.Contexts;
using Pi_ETicaret_Sitesi.Repositories;
using Pi_ETicaret_Sitesi.Interfaces;
using Microsoft.AspNetCore.Identity;
using Pi_ETicaret_Sitesi.Entities;
using Microsoft.AspNetCore.Http;

namespace Pi_ETicaret_Sitesi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication();
            // scoped ilgili isteði yapan kiþiye tek bir nesne örneði döner.
            services.AddScoped<IKategoriRepository, KategoriRepository>();
            services.AddScoped<IUrunKategoriRepository, UrunKategoriRepository>();
            services.AddScoped<IUrunRepository, UrunRepository>();
            services.AddControllersWithViews();
            services.AddSession();

            services.AddDbContext<Context>();
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredLength = 1;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;


            }).AddEntityFrameworkStores<Context>();

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Home/GirisYap");
                opt.Cookie.Name = "piEticaretGiris";
                opt.Cookie.HttpOnly = true;
                opt.Cookie.SameSite = SameSiteMode.Strict;
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
            UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");   
                app.UseHsts();
            }
            BaslangicGirisKimligi.OlusturAdmin(userManager, roleManager);
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "areas", pattern: "{area}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
