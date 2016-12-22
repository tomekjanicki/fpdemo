namespace Demo.Api.Apis
{
    using System.Collections.Immutable;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Http;
    using Swashbuckle.Swagger.Annotations;

    [SwaggerResponseRemoveDefaults]
    public sealed class Test3Controller : ApiController
    {
        [SwaggerResponse(HttpStatusCode.OK)]
        public IHttpActionResult Post(Model model)
        {
            Debug.WriteLine(model);
            return Ok();
        }

        public sealed class Model
        {
            public Model(ImmutableList<Item> items)
            {
                Items = items;
            }

            public ImmutableList<Item> Items { get; }
        }

        public sealed class Item
        {
            public Item(int id1, int id2)
            {
                Id1 = id1;
                Id2 = id2;
            }

            public int Id1 { get; }

            public int Id2 { get; }
        }
    }
}