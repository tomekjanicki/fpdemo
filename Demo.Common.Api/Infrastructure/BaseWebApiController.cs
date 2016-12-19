namespace Demo.Common.Api.Infrastructure
{
    using System.Security.Claims;
    using System.Web.Http;
    using Demo.Common.Shared;
    using Demo.Types.FunctionalExtensions;

    public abstract class BaseWebApiController : ApiController
    {
        public new ClaimsPrincipal User => base.User as ClaimsPrincipal;

        protected IHttpActionResult GetHttpActionResult<T>(IResult<T, Error> result)
        {
            return WebApiControllerHelper.GetHttpActionResult(result, this);
        }

        protected IHttpActionResult GetHttpActionResult(IResult<Error> result)
        {
            return WebApiControllerHelper.GetHttpActionResult(result, this);
        }

        protected IHttpActionResult GetHttpActionResultForDelete(IResult<Error> result)
        {
            return WebApiControllerHelper.GetHttpActionResultForDelete(result, this);
        }

        protected IHttpActionResult GetHttpActionResultForPut(IResult<Error> result)
        {
            return WebApiControllerHelper.GetHttpActionResultForPut(result, this);
        }
    }
}