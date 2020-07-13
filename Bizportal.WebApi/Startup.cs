using Bizportal.Api;
using Bizportal.WebApi.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bizportal.WebApi
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
            services.AddControllers();

            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.ProduceErrorResponse;
            });

            services.AddDbContext<BizportalDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("BizportalDemoDb"));
            });

            services.AddScoped<IBizportalManager<Product>, BizportaLManager<Product>>();
            services.AddScoped<IBizportalManager<Client>, BizportaLManager<Client>>();
            services.AddScoped<IBizportalManager<Category>, BizportaLManager<Category>>();
            services.AddScoped<IBizportalManager<Wallet>, BizportaLManager<Wallet>>();
            services.AddScoped<IBizportalManager<Order>, BizportaLManager<Order>>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
