namespace Demo.Logic.CQ.GetProductById
{
    using Demo.Logic.CQ.GetProductById.Interfaces;
    using Demo.Types;
    using Demo.Types.FunctionalExtensions;

    public sealed class Repository : IRepository
    {
        public Maybe<Product> GetProductById(PositiveInt id)
        {
            return id < 10 ? new Product(id, (NonEmptyString)"code", (NonEmptyString)"name") : null;
        }
    }
}
