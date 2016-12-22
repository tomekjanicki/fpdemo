namespace Demo.Api.Apis
{
    using System.Collections.Immutable;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Http;
    using Swashbuckle.Swagger.Annotations;

    [SwaggerResponseRemoveDefaults]
    public sealed class Test1Controller : ApiController
    {
        [SwaggerResponse(HttpStatusCode.OK)]
        public IHttpActionResult Get([FromUri] ImmutableList<int> ids)
        {
            Debug.WriteLine(ids);
            return Ok();
        }
    }
}
