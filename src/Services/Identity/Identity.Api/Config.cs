﻿using IdentityServer4;
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
                new IdentityResources.Profile(),
                new IdentityResource("roles", "User role(s)", new List<string> { "role" })
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("mvc"),
                new ApiScope("identity"),
                new ApiScope("basket"),
                new ApiScope("categories"),
                new ApiScope("notifications"),
                new ApiScope("products"),
                new ApiScope("images"),
                new ApiScope("orders")
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
                    AllowAccessTokensViaBrowser = true,
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RequirePkce = false,
                    RequireClientSecret = true,
                    AllowedCorsOrigins = { "http://localhost:5000" },
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
                        "mvc",
                        "basket",
                        "categories",
                        "notifications",
                        "products",
                        "images",
                        "orders",
                        "roles"
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
                        "basket",
                        "roles"
                    }
                },
                new Client
                {
                    ClientId = "categories",
                    ClientName = "Categories Swagger UI",
                    ClientSecrets = {new Secret("secret".Sha256()) },

                    AllowAccessTokensViaBrowser = true,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = false,
                    RequireClientSecret = true,

                    AllowedCorsOrigins = { "http://localhost:5001" },
                    RedirectUris = {
                        $"http://localhost:5001/swagger/oauth2-redirect.html?urls.primaryName=Categories.Api"
                    },
                    PostLogoutRedirectUris = {
                        $"http://localhost:5001/swagger/index.html?urls.primaryName=Categories.Api"
                    },

                    AllowedScopes =
                    {
                        "categories",
                        "roles"
                    }
                },
                new Client
                {
                    ClientId = "notifications",
                    ClientName = "Notifications Swagger UI",
                    ClientSecrets = {new Secret("secret".Sha256()) },

                    AllowAccessTokensViaBrowser = true,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = false,
                    RequireClientSecret = true,

                    AllowedCorsOrigins = { "http://localhost:5001" },
                    RedirectUris = {
                        $"http://localhost:5001/swagger/oauth2-redirect.html?urls.primaryName=Notifications.Api"
                    },
                    PostLogoutRedirectUris = {
                        $"http://localhost:5001/swagger/index.html?urls.primaryName=Notifications.Api"
                    },

                    AllowedScopes =
                    {
                        "notifications",
                        "roles"
                    }
                },
                new Client
                {
                    ClientId = "products",
                    ClientName = "Products Swagger UI",
                    ClientSecrets = {new Secret("secret".Sha256()) },

                    AllowAccessTokensViaBrowser = true,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = false,
                    RequireClientSecret = true,

                    AllowedCorsOrigins = { "http://localhost:5001" },
                    RedirectUris = {
                        $"http://localhost:5001/swagger/oauth2-redirect.html?urls.primaryName=Products.Api"
                    },
                    PostLogoutRedirectUris = {
                        $"http://localhost:5001/swagger/index.html?urls.primaryName=Products.Api"
                    },

                    AllowedScopes =
                    {
                        "products",
                        "roles"
                    }
                },
                new Client
                {
                    ClientId = "images",
                    ClientName = "Images Swagger UI",
                    ClientSecrets = {new Secret("secret".Sha256()) },

                    AllowAccessTokensViaBrowser = true,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = false,
                    RequireClientSecret = true,

                    AllowedCorsOrigins = { "http://localhost:5001" },
                    RedirectUris = {
                        $"http://localhost:5001/swagger/oauth2-redirect.html?urls.primaryName=Images.Api"
                    },
                    PostLogoutRedirectUris = {
                        $"http://localhost:5001/swagger/index.html?urls.primaryName=Images.Api"
                    },

                    AllowedScopes =
                    {
                        "images",
                        "roles"
                    }
                },
                new Client
                {
                    ClientId = "orders",
                    ClientName = "Orders Swagger UI",
                    ClientSecrets = {new Secret("secret".Sha256()) },

                    AllowAccessTokensViaBrowser = true,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = false,
                    RequireClientSecret = true,

                    AllowedCorsOrigins = { "http://localhost:5001" },
                    RedirectUris = {
                        $"http://localhost:5001/swagger/oauth2-redirect.html?urls.primaryName=Orders.Api"
                    },
                    PostLogoutRedirectUris = {
                        $"http://localhost:5001/swagger/index.html?urls.primaryName=Orders.Api"
                    },

                    AllowedScopes =
                    {
                        "images",
                        "roles"
                    }
                }
            };
    }
}
