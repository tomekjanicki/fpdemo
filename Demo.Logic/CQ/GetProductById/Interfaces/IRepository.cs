namespace Demo.Logic.CQ.GetProductById.Interfaces
{
    using Demo.Types;
    using Demo.Types.FunctionalExtensions;

    public interface IRepository
    {
        Maybe<Product> GetProductById(PositiveInt id);
    }
}