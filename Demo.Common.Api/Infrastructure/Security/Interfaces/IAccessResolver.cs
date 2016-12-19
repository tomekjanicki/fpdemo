namespace Demo.Common.Api.Infrastructure.Security.Interfaces
{
    using System.Collections.Generic;
    using Demo.Types;

    public interface IAccessResolver
    {
        bool CanAccess(NonEmptyLowerCaseString resourceWithAction, bool isAuthenticated, IReadOnlyCollection<NonEmptyLowerCaseString> roles);
    }
}