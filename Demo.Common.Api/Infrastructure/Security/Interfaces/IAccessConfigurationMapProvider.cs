namespace Demo.Common.Api.Infrastructure.Security.Interfaces
{
    using System.Collections.Immutable;
    using Demo.Types;

    public interface IAccessConfigurationMapProvider
    {
        ImmutableDictionary<NonEmptyLowerCaseString, Scopes> Get();
    }
}