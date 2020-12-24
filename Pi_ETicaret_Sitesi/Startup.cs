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
            // scoped ilgili isteði yapan kiþiye tek bir nesne örneði döner.
            services.AddScoped<IKategoriRepository, KategoriRepository>();
            services.AddScoped<IUrunKategoriRepository, UrunKategoriRepository>();
            services.AddScoped<IUrunRepository, UrunRepository>();
            services.AddControllersWithViews();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
