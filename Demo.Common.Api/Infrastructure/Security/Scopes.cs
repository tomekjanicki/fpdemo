namespace Demo.Common.Api.Infrastructure.Security
{
    using System.Collections.Generic;
    using System.Linq;
    using Demo.Types;
    using Demo.Types.FunctionalExtensions;

    public sealed class Scopes : ValueObject<Scopes>
    {
        private Scopes(bool anonymous, IReadOnlyCollection<NonEmptyLowerCaseString> scopeCollection)
        {
            Anonymous = anonymous;
            ScopeCollection = scopeCollection;
        }

        public bool Anonymous { get; }

        public IReadOnlyCollection<NonEmptyLowerCaseString> ScopeCollection { get; }

        public static Scopes CreateAnonymous()
        {
            return new Scopes(true, new List<NonEmptyLowerCaseString>());
        }

        public static Scopes CreateScopeOnly(NonEmptyLowerCaseString scope)
        {
            return new Scopes(false, new List<NonEmptyLowerCaseString> { scope });
        }

        public static IResult<Scopes, NonEmptyString> CreateScopesOnly(IReadOnlyCollection<NonEmptyLowerCaseString> scopes)
        {
            return scopes.Count == 0 ? GetFailResult((NonEmptyString)(nameof(ScopeCollection) + " cannot be empty list")) : GetOkResult(new Scopes(false, scopes.Distinct().ToList()));
        }

        public bool ContainScopes(IEnumerable<NonEmptyLowerCaseString> scopes)
        {
            return scopes.Any(scope => ScopeCollection.Contains(scope));
        }

        protected override bool EqualsCore(Scopes other)
        {
            return Anonymous == other.Anonymous && ScopeCollection.SequenceEqual(other.ScopeCollection);
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new List<object> { Anonymous, ScopeCollection });
        }
    }
}