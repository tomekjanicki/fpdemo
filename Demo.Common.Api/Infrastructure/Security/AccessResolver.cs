namespace Demo.Common.Api.Infrastructure.Security
{
    using System.Collections.Generic;
    using Demo.Common.Api.Infrastructure.Security.Interfaces;
    using Demo.Types;

    public sealed class AccessResolver : IAccessResolver
    {
        private readonly IAccessConfigurationMapProvider _accessConfigurationMapProvider;

        public AccessResolver(IAccessConfigurationMapProvider accessConfigurationMapProvider)
        {
            _accessConfigurationMapProvider = accessConfigurationMapProvider;
        }

        public bool CanAccess(NonEmptyLowerCaseString resourceWithAction, bool isAuthenticated, IReadOnlyCollection<NonEmptyLowerCaseString> scopes)
        {
            var accessConfigurationMap = _accessConfigurationMapProvider.Get();

            // not on accessConfigurationMap - any scope, must be authenticated
            if (!accessConfigurationMap.ContainsKey(resourceWithAction))
            {
                return isAuthenticated;
            }

            var configuredScopes = accessConfigurationMap[resourceWithAction];

            // present on accessConfigurationMap, but no scopes - should be anonymous or authenticated
            if (configuredScopes.Anonymous)
            {
                return true;
            }

            // present on accessConfigurationMap with scopes - only this scope, must be authenticated
            return isAuthenticated && configuredScopes.ContainScopes(scopes);
        }
    }
}