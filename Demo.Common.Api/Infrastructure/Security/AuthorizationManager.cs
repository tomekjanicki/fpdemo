namespace Demo.Common.Api.Infrastructure.Security
{
    using System.Linq;
    using System.Security.Claims;
    using Demo.Common.Api.Infrastructure.Security.Interfaces;
    using Demo.Common.IoC;
    using Demo.Types;

    public sealed class AuthorizationManager : ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
            var accessResolver = IoCContainerProvider.GetContainer().Get<IAccessResolver>();
            var action = context.Action.First().Value;
            var resource = context.Resource.First().Value;
            var resourceWithAction = $"{resource}/{action}";
            var scopes = context.Principal.Claims.Where(claim => claim.Type == "scope").Select(claim => (NonEmptyLowerCaseString)claim.Value).ToList();
            return accessResolver.CanAccess((NonEmptyLowerCaseString)resourceWithAction, context.Principal.Identity.IsAuthenticated, scopes);
        }
    }
}