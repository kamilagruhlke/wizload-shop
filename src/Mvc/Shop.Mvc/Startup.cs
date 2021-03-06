using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Mvc.Application.Handlers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Threading.Tasks;
using WizLoad.ApiClient;

namespace Shop.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment WebHostEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<categoriesClient>((provider, client) => {
                client.BaseAddress = new Uri(Configuration["CategoriesApi"]);
            }).SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<productsClient>((provider, client) => {
                client.BaseAddress = new Uri(Configuration["ProductsApi"]);
            }).SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<imagesClient>((provider, client) => {
                client.BaseAddress = new Uri(Configuration["ImagesApi"]);
            }).SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<basketClient>((provider, client) => {
                client.BaseAddress = new Uri(Configuration["BasketApi"]);
            }).SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<ordersClient>((provider, client) => {
                client.BaseAddress = new Uri(Configuration["OrdersApi"]);
            }).SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSession();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, setup => setup.ExpireTimeSpan = TimeSpan.FromMinutes(60))
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = Configuration["IdentityApi"];
                options.SignedOutRedirectUri = Configuration["CallBackUrl"];
                options.ClaimsIssuer = $"{Configuration["IdentityApiExternal"]}";
                options.ClientId = "mvc";
                options.ClientSecret = "secret";
                options.ResponseType = "code id_token";
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.RequireHttpsMetadata = false;

                if (WebHostEnvironment.IsDevelopment())
                {
                    options.MetadataAddress = $"{Configuration["IdentityApi"]}/.well-known/openid-configuration";
                    options.RequireHttpsMetadata = false;

                    options.Events.OnRedirectToIdentityProvider = context =>
                    {
                        context.ProtocolMessage.IssuerAddress = $"{Configuration["IdentityApiExternal"]}/connect/authorize";
                        return Task.CompletedTask;
                    };
                }

                options.TokenValidationParameters.ValidateIssuer = true;
                options.TokenValidationParameters.ValidIssuers = new[]
                {
                    $"{Configuration["IdentityApi"]}",
                    $"{Configuration["IdentityApiExternal"]}"
                };

                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("mvc");
                options.Scope.Add("basket");
                options.Scope.Add("notifications");
                options.Scope.Add("categories");
                options.Scope.Add("products");
                options.Scope.Add("images");
                options.Scope.Add("orders");
                options.Scope.Add("roles");

                options.ClaimActions.MapUniqueJsonKey("role", "role");
                options.TokenValidationParameters.RoleClaimType = "role";
            });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            var pathBase = Configuration["PATH_BASE"];
            if (string.IsNullOrEmpty(pathBase) == false)
            {
                app.UsePathBase(pathBase);
            }

            app.UseStaticFiles();
            app.UseSession();

            app.UseSession();

            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                     name: "Panel",
                     areaName: "Panel",
                     pattern: "Panel/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
