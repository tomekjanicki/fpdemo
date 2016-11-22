namespace Demo.Api.Apis
{
    using System.Web.Http;
    using Demo.Api.Infrastructure;
    using Demo.Logic.Facades.Apis;

    public sealed class ProductController : BaseWebApiController
    {
        private readonly ProductGetFacade _productGetFacade;

        public ProductController(ProductGetFacade productGetFacade)
        {
            _productGetFacade = productGetFacade;
        }

        public IHttpActionResult Get(int id)
        {
            var result = _productGetFacade.Get(id);

            return GetHttpActionResult(result);
        }
    }
}
