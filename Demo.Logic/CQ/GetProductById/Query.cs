namespace Demo.Logic.CQ.GetProductById
{
    using Demo.Common.Handlers.Interfaces;
    using Demo.Common.Shared;
    using Demo.Types;
    using Demo.Types.FunctionalExtensions;

    public sealed class Query : ValueObject<Query>, IRequest<IResult<Product, Error>>
    {
        private Query(PositiveInt id)
        {
            Id = id;
        }

        public PositiveInt Id { get; }

        public static IResult<Query, NonEmptyString> Create(int id)
        {
            var result = PositiveInt.Create(id, (NonEmptyString)nameof(Id));
            return result.IsFailure ? GetFailResult(result.Error) : GetOkResult(new Query(result.Value));
        }

        protected override bool EqualsCore(Query other)
        {
            return Id == other.Id;
        }

        protected override int GetHashCodeCore()
        {
            return Id.GetHashCode();
        }
    }
}
