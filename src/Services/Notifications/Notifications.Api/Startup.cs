using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Notifications.Api.Infrastructure.Filters;

namespace Notifications.Api
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

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = $"{Configuration["IdentityApi"]}";
                options.RequireHttpsMetadata = false;
                options.Audience = $"{Configuration["IdentityApiExternal"]}/resources";
                options.ClaimsIssuer = $"{Configuration["IdentityApiExternal"]}";
                options.TokenValidationParameters.ValidateIssuer = true;
                options.TokenValidationParameters.ValidIssuers = new[]
                {
                    $"{Configuration["IdentityApi"]}",
                    $"{Configuration["IdentityApiExternal"]}"
                };
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Notifications.Api", Version = "v1" });

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{Configuration["IdentityApiExternal"]}/connect/authorize"),
                            TokenUrl = new Uri($"{Configuration["IdentityApiExternal"]}/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {"notifications", "Notifications.Api access"}
                            }
                        }
                    }
                });

                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var basePath = Configuration["PATH_BASE"];
            if (string.IsNullOrEmpty(basePath) == false)
            {
                app.Use((context, next) =>
                {
                    context.Request.PathBase = new PathString(basePath);
                    return next();
                });
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options => {
                options.RoutePrefix = "swagger";
                options.SwaggerEndpoint("./v1/swagger.json", "Notifications.Api v1");

                options.OAuthClientId("notifications");
                options.OAuthAppName("Notifications Swagger UI");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
