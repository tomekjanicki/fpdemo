namespace Demo.Api.Infrastructure.Security
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Demo.Common.Api.Infrastructure.Security;
    using Demo.Common.Api.Infrastructure.Security.Interfaces;
    using Demo.Types;

    public sealed class AccessConfigurationMapProvider : IAccessConfigurationMapProvider
    {
        private static readonly ImmutableDictionary<NonEmptyLowerCaseString, Scopes> Dictionary;

        static AccessConfigurationMapProvider()
        {
            var anonymous = Scopes.CreateAnonymous();

            var writeScopeOnly = Scopes.CreateScopeOnly((NonEmptyLowerCaseString)"write");

            Dictionary = new Dictionary<NonEmptyLowerCaseString, Scopes>
            {
                { (NonEmptyLowerCaseString)"product/put", writeScopeOnly },
                { (NonEmptyLowerCaseString)"version/get", anonymous }
            }.ToImmutableDictionary();
        }

        public ImmutableDictionary<NonEmptyLowerCaseString, Scopes> Get()
        {
            return Dictionary;
        }
    }
}