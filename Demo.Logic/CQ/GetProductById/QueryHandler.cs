namespace Demo.Logic.CQ.GetProductById
{
    using Demo.Common.Handlers.Interfaces;
    using Demo.Logic.CQ.GetProductById.Interfaces;
    using Demo.Logic.Shared;
    using Demo.Types.FunctionalExtensions;

    public sealed class QueryHandler : IRequestHandler<Query, IResult<Product, Error>>
    {
        private readonly IRepository _repository;

        public QueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public IResult<Product, Error> Handle(Query message)
        {
            var product = _repository.GetProductById(message.Id);

            return product.HasNoValue ? ErrorResultExtensions.ToNotFound<Product>() : Result<Product, Error>.Ok(product.Value);
        }
    }
}
