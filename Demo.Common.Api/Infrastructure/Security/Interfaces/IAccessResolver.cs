namespace Demo.Common.Api.Infrastructure.Security.Interfaces
{
    using System.Collections.Immutable;
    using Demo.Types;

    public interface IAccessResolver
    {
        bool CanAccess(NonEmptyLowerCaseString resourceWithAction, bool isAuthenticated, ImmutableList<NonEmptyLowerCaseString> roles);
    }
}