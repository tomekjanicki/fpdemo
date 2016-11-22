namespace Demo.Logic.CQ.UpdateProduct
{
    using Demo.Logic.CQ.UpdateProduct.Interfaces;
    using Demo.Types;

    public sealed class Repository : IRepository
    {
        public bool ExistsById(PositiveInt id)
        {
            return id < 10;
        }

        public void Update(Command command)
        {
        }
    }
}
