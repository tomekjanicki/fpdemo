namespace Demo.Api.Apis
{
    using System.Net;
    using System.Web.Http;
    using Demo.Common.Api.Infrastructure;
    using Demo.Common.Shared;
    using Demo.Types.FunctionalExtensions;
    using Swashbuckle.Swagger.Annotations;

    [SwaggerResponseRemoveDefaults]
    public class VersionController : BaseWebApiController
    {
        [SwaggerResponse(HttpStatusCode.OK, null, typeof(string))]
        public IHttpActionResult Get()
        {
            var result = Result<string, Error>.Ok("v1");

            return GetHttpActionResult(result);
        }
    }
}