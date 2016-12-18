namespace Demo.IS.Configuration
{
    using System.Collections.Generic;
    using IdentityServer3.Core;
    using IdentityServer3.Core.Models;

    public static class Clients
    {
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "socialnetwork",
                    ClientName = "SocialNetwork",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    Flow = Flows.ResourceOwner,
                    AllowedScopes = new List<string> { Constants.StandardScopes.OpenId },
                    Enabled = true
                }
            };
        }
    }
}