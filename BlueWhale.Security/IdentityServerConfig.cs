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
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = {$"http://{configuration["AudienceUrl"]}/signin-oidc"},
                    PostLogoutRedirectUris = {$"http://{configuration["AudienceUrl"]}/signout-callback-oidc" },
                    RequireConsent = false,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("Secretcode".Sha256())
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
    }
}
