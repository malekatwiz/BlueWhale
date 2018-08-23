using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

namespace BlueWhale.Security
{
    public static class IdentityServerConfig
    {
        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "BlueWhale.Main",
                    ClientName = "BlueWhale.Main",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientUri = configuration["AudienceUrl"],
                    RedirectUris = {$"{configuration["AudienceUrl"]}/signin-oidc"},
                    PostLogoutRedirectUris = {$"{configuration["AudienceUrl"]}/signout-callback-oidc" },
                    RequireConsent = false,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "BlueWhale.Exchange"
                    },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("Secretcode".Sha256())
                    }
                }
            };
        }

        public static List<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("BlueWhale.Exchange", "BlueWhale Exchange API")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
    }
}
