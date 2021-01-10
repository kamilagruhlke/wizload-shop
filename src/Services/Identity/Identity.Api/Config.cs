using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Identity.Api
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("identity"),
                new ApiScope("basket"),
                new ApiScope("categories"),
                new ApiScope("notification"),
                new ApiScope("products")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientUri = $"http://localhost:5000",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AllowAccessTokensViaBrowser = false,
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RedirectUris = new List<string>
                    {
                        $"http://localhost:5000/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        $"http://localhost:5000/signout-callback-oidc"
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "basket",
                        "categories",
                        "notifications",
                        "products"
                    },
                    AccessTokenLifetime = 60*60*2,
                    IdentityTokenLifetime= 60*60*2
                },
                new Client
                {
                    ClientId = "basket",
                    ClientName = "Basket Swagger UI",
                    ClientSecrets = {new Secret("secret".Sha256()) },

                    AllowAccessTokensViaBrowser = true,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = false,
                    RequireClientSecret = true,

                    AllowedCorsOrigins = { "http://localhost:5001" },
                    RedirectUris = {
                        $"http://localhost:5001/swagger/oauth2-redirect.html?urls.primaryName=Basket.Api"
                    },
                    PostLogoutRedirectUris = {
                        $"http://localhost:5001/swagger/index.html?urls.primaryName=Basket.Api"
                    },

                    AllowedScopes =
                    {
                        "basket"
                    }
                }
            };
    }
}
