namespace Demo.IS.Configuration
{
    using System.Collections.Generic;
    using IdentityServer3.Core.Models;

    public static class Scopes
    {
        public static IEnumerable<Scope> GetScopes()
        {
            return new[]
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.OfflineAccess,
                new Scope
                {
                    Name = "write",
                    DisplayName = "Write user data",
                    Type = ScopeType.Resource
                }
            };
        }
    }
}