using System.Collections.Generic;
using Gateway.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Gateway.Api
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
            services.AddHealthChecks();

            services.AddCustomAuthentication(Configuration);

            services.AddOcelot(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerUI(c => {
                if (string.IsNullOrWhiteSpace(Configuration["BASEPATH"]) == false)
                {
                    c.RoutePrefix = Configuration["BASEPATH"];
                }

                var swaggerEndpoints = Configuration.GetSection("SwaggerEndpoints")
                    .Get<Dictionary<string, string>>();

                if (swaggerEndpoints is not null)
                {
                    foreach (var (name, url) in swaggerEndpoints)
                    {
                        c.SwaggerEndpoint(url, name);
                    }
                }
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                AllowCachingResponses = false
            });

            app.UseOcelot().Wait();
        }
    }
}
