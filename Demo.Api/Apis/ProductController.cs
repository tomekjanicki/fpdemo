namespace Demo.Api.Apis
{
    using System.Web.Http;
    using Demo.Api.Infrastructure;
    using Demo.Logic.Facades.Apis;

    public sealed class ProductController : BaseWebApiController
    {
        private readonly ProductGetFacade _productGetFacade;
        private readonly ProductPutFacade _productPutFacade;

        public ProductController(ProductGetFacade productGetFacade, ProductPutFacade productPutFacade)
        {
            _productGetFacade = productGetFacade;
            _productPutFacade = productPutFacade;
        }

        public IHttpActionResult Get(int id)
        {
            var result = _productGetFacade.Get(id);

            return GetHttpActionResult(result);
        }

        public IHttpActionResult Put(int id, Dtos.Apis.Product.Put.Product product)
        {
            var result = _productPutFacade.Put(id, product);

            return GetHttpActionResultForPut(result);
        }
    }
}
