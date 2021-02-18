using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Orders.Api.Application.Utils;
using Orders.Api.Infrastructure.Filters;
using Orders.Domain.AggregateModel.OrderAggregate;
using Orders.Domain.Utils.Interfaces;
using Orders.Infrastructure;
using Orders.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Orders.Api
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
            services.AddDbContext<OrdersDbContext>(contextOptionsBuilder => {
                contextOptionsBuilder.UseNpgsql(Configuration.GetConnectionString("OrdersDb"), options => {
                    options.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                    options.MigrationsHistoryTable("migrations_history", "orders");
                });

                contextOptionsBuilder.ReplaceService<IHistoryRepository, OrderHistoryRepository>();
            });

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

            services.AddScoped<IUserAccessor, UserAccessor>()
                .AddScoped<IOrderRepository, OrderRepository>()
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddHttpContextAccessor();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Orders.Api", Version = "v1" });

                options.CustomOperationIds(apiDescription => {
                    var description = (apiDescription.ActionDescriptor as ControllerActionDescriptor);
                    return $"{description.ControllerName}{description.ActionName}";
                });

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
                                {"orders", "Orders.Api access"}
                            }
                        }
                    }
                });

                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });

            services.AddHealthChecks();
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
                options.SwaggerEndpoint("./v1/swagger.json", "Products.Api v1");

                options.OAuthClientId("orders");
                options.OAuthAppName("orders Swagger UI");
            });

            app.UseRouting();

            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    AllowCachingResponses = false
                });
            });
        }
    }
}
