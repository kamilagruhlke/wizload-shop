using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Serialization;
using Categories.Api.Application.Utils;
using Categories.Api.Infrastructure.Filters;
using Categories.Domain.AggregateModel.CategoryAggregate;
using Categories.Domain.Utils.Interfaces;
using Categories.Infrastructure;
using Categories.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Categories.Api
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
            services.AddDbContext<CategoriesDbContext>(contextOptionsBuilder => {
                contextOptionsBuilder.UseNpgsql(Configuration.GetConnectionString("CategoriesDb"), options => {
                    options.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                    options.MigrationsHistoryTable("migrations_history", "categories");
                });

                contextOptionsBuilder.ReplaceService<IHistoryRepository, CategoriesHistoryRepository>();
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
                .AddScoped<ICategoryRepository, CategoryRepository>()
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddHttpContextAccessor();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Categories.Api", Version = "v1" });

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
                                {"categories", "Categories.Api access"}
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
                })
                .AddFluentValidation(validation => validation.RegisterValidatorsFromAssemblyContaining<Startup>());

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
                options.SwaggerEndpoint("./v1/swagger.json", "Categories.Api v1");

                options.OAuthClientId("categories");
                options.OAuthAppName("Categories Swagger UI");
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
