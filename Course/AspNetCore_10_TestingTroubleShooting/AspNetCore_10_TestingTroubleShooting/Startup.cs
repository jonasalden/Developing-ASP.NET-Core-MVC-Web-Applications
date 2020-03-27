using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AspNetCore_10_TestingTroubleShooting.Data;
using AspNetCore_10_TestingTroubleShooting.Services;

namespace AspNetCore_10_TestingTroubleShooting
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IShirtRepository, ShirtRepository>();

            services.AddDbContext<ShirtContext>(options =>
                 options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ShirtContext shirtContext)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error.html");
            }

            shirtContext.Database.EnsureDeleted();
            shirtContext.Database.EnsureCreated();

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "defaultRoute",
                    template: "{controller=Shirt}/{action=Index}/{id?}");
            });
        }
    }
}
