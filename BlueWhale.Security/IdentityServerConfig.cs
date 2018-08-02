using System;
using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace BlueWhale.Security
{
    public static class IdentityServerConfig
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "BlueWhale.Main",
                    ClientName = "BlueWhale.Main",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = {"http://localhost:5002/signin-oidc"},
                    PostLogoutRedirectUris = {"http://localhost:5002/signout-callback-oidc"},
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
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
