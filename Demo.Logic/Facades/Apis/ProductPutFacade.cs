namespace Demo.Logic.Facades.Apis
{
    using Demo.Common.Handlers.Interfaces;
    using Demo.Common.Shared;
    using Demo.Dtos.Apis.Product.Put;
    using Demo.Logic.CQ.UpdateProduct;
    using Demo.Logic.Facades.Base;
    using Demo.Types;
    using Demo.Types.FunctionalExtensions;

    public sealed class ProductPutFacade
    {
        private readonly IMediator _mediator;

        public ProductPutFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IResult<Error> Put(int id, Product product)
        {
            var commandResult = Command.Create(id, product.Name.ToEmptyString(), product.Size);

            return Helper.Put(_mediator, commandResult);
        }
    }
}
