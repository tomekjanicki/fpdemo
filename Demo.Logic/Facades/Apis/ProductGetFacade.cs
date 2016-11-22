namespace Demo.Logic.Facades.Apis
{
    using AutoMapper;
    using Demo.Common.Handlers.Interfaces;
    using Demo.Logic.CQ.GetProductById;
    using Demo.Logic.Facades.Base;
    using Demo.Logic.Shared;
    using Demo.Types.FunctionalExtensions;

    public sealed class ProductGetFacade
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductGetFacade(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public IResult<Dtos.Apis.Product.Get.Product, Error> Get(int id)
        {
            var queryResult = Query.Create(id);

            return Helper.GetItem<Dtos.Apis.Product.Get.Product, Query, Product>(_mediator, _mapper, queryResult);
        }
    }
}
