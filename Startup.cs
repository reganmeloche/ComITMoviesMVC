using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MoviesMVC.Models;

namespace MoviesMVC
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
            services.AddControllersWithViews();
            
            var movieList = new List<Movie>();
            var movie1 = new Movie();
            movie1.Id = Guid.NewGuid();
            movie1.Title = "Jurassic Park";
            movie1.Director = "Steven Spielberg";
            movie1.Year = 1993;
            movieList.Add(movie1);

            var movie2 = new Movie();
            movie2.Id = Guid.NewGuid();
            movie2.Title = "True Grit";
            movie2.Director = "Ethan Coen";
            movie2.Year = 2010;
            movieList.Add(movie2);

            services.AddSingleton(movieList);
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Movie}/{action=Index}/{id?}");
            });
        }
    }
}
