namespace Demo.Api.Apis
{
    using System.Collections.Immutable;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Http;
    using Swashbuckle.Swagger.Annotations;

    [SwaggerResponseRemoveDefaults]
    public sealed class Test2Controller : ApiController
    {
        [SwaggerResponse(HttpStatusCode.OK)]
        public IHttpActionResult Post(ImmutableList<Model> models)
        {
            Debug.WriteLine(models);
            return Ok();
        }

        public sealed class Model
        {
            public Model(int id1, int id2)
            {
                Id1 = id1;
                Id2 = id2;
            }

            public int Id1 { get; }

            public int Id2 { get; }
        }
    }
}