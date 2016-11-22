namespace Demo.Logic.CQ.UpdateProduct.Interfaces
{
    using Demo.Types;

    public interface IRepository
    {
        bool ExistsById(PositiveInt id);

        void Update(Command command);
    }
}