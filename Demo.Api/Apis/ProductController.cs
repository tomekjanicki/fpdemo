namespace Demo.Api.Apis
{
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Web.Http;
    using Demo.Common.Api.Infrastructure;
    using Demo.Logic.Facades.Apis;
    using Swashbuckle.Swagger.Annotations;

    [SwaggerResponseRemoveDefaults]
    public sealed class ProductController : BaseWebApiController
    {
        private readonly ProductGetFacade _productGetFacade;
        private readonly ProductPutFacade _productPutFacade;

        public ProductController(ProductGetFacade productGetFacade, ProductPutFacade productPutFacade)
        {
            _productGetFacade = productGetFacade;
            _productPutFacade = productPutFacade;
        }

        [SwaggerResponse(HttpStatusCode.OK, null, typeof(Dtos.Apis.Product.Get.Product))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Get(int id)
        {
            WriteUserName();

            var result = _productGetFacade.Get(id);

            return GetHttpActionResult(result);
        }

        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Put(int id, Dtos.Apis.Product.Put.Product product)
        {
            WriteUserName();

            var result = _productPutFacade.Put(id, product);

            return GetHttpActionResultForPut(result);
        }

        private void WriteUserName()
        {
            var name = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;

            if (name != null)
            {
                Debug.WriteLine(name);
            }
        }
    }
}
