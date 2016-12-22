namespace Demo.Api.Apis
{
    using System.Collections.Immutable;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;
    using Demo.Common.Api.Infrastructure;
    using Swashbuckle.Swagger.Annotations;

    [SwaggerResponseRemoveDefaults]
    public sealed class Test1Controller : ApiController
    {
        [SwaggerResponse(HttpStatusCode.OK)]
        public IHttpActionResult Get([ModelBinder(typeof(ImmutableListModelBinder<int>))] ImmutableList<int> ids)
        {
            Debug.WriteLine(ids);
            return Ok();
        }
    }
}
