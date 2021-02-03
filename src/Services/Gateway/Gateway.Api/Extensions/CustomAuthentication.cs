using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway.Api.Extensions
{
    public static class CustomAuthentication
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            var identityUrl = configuration.GetValue<string>("urls:identity");
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = $"{configuration["IdentityApi"]}";
                options.RequireHttpsMetadata = false;
                options.Audience = $"{configuration["IdentityApiExternal"]}/resources";
                options.ClaimsIssuer = $"{configuration["IdentityApiExternal"]}";
                options.TokenValidationParameters.ValidateIssuer = true;
                options.TokenValidationParameters.ValidIssuers = new[]
                {
                    $"{configuration["IdentityApi"]}",
                    $"{configuration["IdentityApiExternal"]}"
                };
            });

            return services;
        }
    }
}
