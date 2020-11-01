using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Mix.IdentityServer4
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("api1"),
                new ApiScope(IdentityServerConstants.StandardScopes.OpenId),
                new ApiScope(IdentityServerConstants.StandardScopes.Profile),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "Api 1"),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("77DAABEF-697A-4CC1-A400-3CC561B9AD83".Sha256()) },
                    AllowedScopes =
                    {
                        "api1"
                    }
                },
                new Client
                {
                    ClientId = "wpf client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("77DAABEF-697A-4CC1-A400-3CC561B9AD83".Sha256())
                    },
                    AllowedScopes =
                    { "api1",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }
    }
}