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

            services.AddScoped<IBizportalManager<Product>, BizportalManager<Product>>();
            services.AddScoped<IBizportalManager<Client>, BizportalManager<Client>>();
            services.AddScoped<IBizportalManager<Category>, BizportalManager<Category>>();
            services.AddScoped<IBizportalManager<Wallet>, BizportalManager<Wallet>>();
            services.AddScoped<IBizportalManager<Order>, BizportalManager<Order>>();

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
