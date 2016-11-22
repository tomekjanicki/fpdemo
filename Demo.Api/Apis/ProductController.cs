namespace Demo.Api.Apis
{
    using System.Net;
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
            var result = _productGetFacade.Get(id);

            return GetHttpActionResult(result);
        }

        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Put(int id, Dtos.Apis.Product.Put.Product product)
        {
            var result = _productPutFacade.Put(id, product);

            return GetHttpActionResultForPut(result);
        }
    }
}
