namespace Demo.Common.Api.Infrastructure.Security.Interfaces
{
    using System.Collections.Generic;
    using Demo.Types;

    public interface IAccessConfigurationMapProvider
    {
        IReadOnlyDictionary<NonEmptyLowerCaseString, Scopes> Get();
    }
}