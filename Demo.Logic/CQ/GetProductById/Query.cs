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

        public static IResult<Query, NonEmptyString> TryCreate(int id)
        {
            var result = PositiveInt.TryCreate(id, (NonEmptyString)nameof(Id));
            return result.OnSuccess(positiveIntId => GetOkResult(new Query(positiveIntId)));
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
