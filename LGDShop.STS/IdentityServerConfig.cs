// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel;
using IdentityServer4.Models;
using LGDShop.Domain.Constants;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace StsServerIdentity
{
    public class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            List<IdentityResource> resources = new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
            return resources;
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(IdentityServerSettings.ApiName, IdentityServerSettings.ApiFriendlyName)
                {
                    ApiSecrets =
                    {
                        new Secret(IdentityServerSettings.ApiSecret.Sha256())
                    },
                    Scopes =
                    {
                        new Scope
                        {
                            Name = ApiScopes.General,
                            DisplayName = "Scope for the general access in api"
                        }
                    },
                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Role,
                        ApiClaims.Permission
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients(IConfigurationSection stsConfig)
        {
            // TODO use configs in app
            //var yourConfig = stsConfig["ClientUrl"];

            return new List<Client>
            {
                //spa-angular client
                new Client { 
                    RequireConsent = false,
                    ClientId = IdentityServerSettings.AngularAppClientID,
                    ClientName = "Angular SPA",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = { "openid", "profile", "email", ApiScopes.General},
                    RedirectUris = {"http://localhost:4200/auth-callback"},
                    PostLogoutRedirectUris = {"http://localhost:4200/"},
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:4200",
                        "https://localhost:4200"
                    },
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = false, // This client does not need a secret to request tokens from the token endpoint.
                    AccessTokenLifetime = 300
                },
                //swagger ui client
                new Client
                {
                    RequireConsent = false,
                    ClientId = IdentityServerSettings.ApiSwaggerClientID,
                    ClientName = "Swagger UI for API",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "http://localhost:5001/docs/oauth2-redirect.html" },
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = false,
                    //AllowedCorsOrigins = new List<string> //inside web-api
                    //{
                    //    "http://localhost:5001",
                    //    "https://localhost:44344"
                    //},
                    AllowedScopes = {
                        ApiScopes.General
                    }
                }


                // example code
                //new Client
                //{
                //    ClientName = "angularclient",
                //    ClientId = "angularclient",
                //    AccessTokenType = AccessTokenType.Reference,
                //    AccessTokenLifetime = 330,// 330 seconds, default 60 minutes
                //    IdentityTokenLifetime = 30,
                //    AllowedGrantTypes = GrantTypes.Implicit,
                //    AllowAccessTokensViaBrowser = true,
                //    RedirectUris = new List<string>
                //    {
                //        "https://localhost:44311",
                //        "https://localhost:44311/silent-renew.html"

                //    },
                //    PostLogoutRedirectUris = new List<string>
                //    {
                //        "https://localhost:44311/unauthorized",
                //        "https://localhost:44311"
                //    },
                //    AllowedCorsOrigins = new List<string>
                //    {
                //        "https://localhost:44311",
                //        "http://localhost:44311"
                //    },
                //    AllowedScopes = new List<string>
                //    {
                //        "openid",
                //        "role",
                //        "profile",
                //        "email"
                //    }
                //}
            };
        }
    }
}