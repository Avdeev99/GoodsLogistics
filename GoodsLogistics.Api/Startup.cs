using GoodsLogistics.Api.AppStart;
using GoodsLogistics.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace GoodsLogistics.Api
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
            DiConfigurator.Configure(services, Configuration);

            AuthConfigurator.Configure(services, Configuration);

            services
                .AddControllers()
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services
                .AddControllers()
                .AddNewtonsoftJson(x =>
                {
                    x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
#if DEBUG
            app.UseDeveloperExceptionPage();
#endif

            if (env.IsDevelopment())
            {
                app.UseCors(corsPolicy =>
                {
                    corsPolicy.AllowAnyHeader();
                    corsPolicy.AllowAnyMethod();
                    corsPolicy.AllowAnyOrigin();
                });
            }
            else
            {
                app.UseCors();
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            AuthConfigurator.Configure(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
