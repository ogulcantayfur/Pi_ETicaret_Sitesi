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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Pi_ETicaret_Sitesi.Models;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

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
            services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
            services.AddMvc().AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();


            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
            new CultureInfo("tr"),
            new CultureInfo("en"),

        };
                options.DefaultRequestCulture = new RequestCulture(culture: "tr", uiCulture: "tr");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddRazorPages();

            services.AddAuthentication();

            services.AddControllersWithViews();
            services.AddSession();
            services.AddHttpContextAccessor();

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Identity/Account/Login");
                opt.Cookie.Name = "piEticaretGiris";
                opt.Cookie.HttpOnly = true;
                opt.Cookie.SameSite = SameSiteMode.Strict;
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(10);
            });
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var dbContext = serviceProvider.GetRequiredService<Context>();

            IdentityResult roleResult1;
            IdentityResult roleResult2;
            //Adding Admin Role
            var roleCheck1 = await RoleManager.RoleExistsAsync("Admin");
            var roleCheck2 = await RoleManager.RoleExistsAsync("User");
            if (!roleCheck1)
            {
                //create the roles and seed them to the database
                roleResult1 = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!roleCheck2)
            {
                //create the roles and seed them to the database
                roleResult2 = await RoleManager.CreateAsync(new IdentityRole("User"));
            }

            if (!dbContext.Users.Any(u => u.UserName == "ogulcan"))
            {
                var adminUser = new AppUser
                {
                    UserName = "ogulcan",
                };
                var result = await UserManager.CreateAsync(adminUser, "1");
                await UserManager.AddToRoleAsync(adminUser, new IdentityRole("Admin").Name);
            }
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IServiceProvider serviceProvider)
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

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            var desteklenenDiller = new[] { "tr", "en" };
            var localizationAyarlari = new RequestLocalizationOptions().SetDefaultCulture(desteklenenDiller[0])
                .AddSupportedCultures(desteklenenDiller).AddSupportedUICultures(desteklenenDiller);

            app.UseRequestLocalization(localizationAyarlari);
            CreateUserRoles(serviceProvider).Wait();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "areas", pattern: "{area}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }




    }
}
