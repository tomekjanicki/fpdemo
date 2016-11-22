namespace Demo.Logic.CQ.GetProductById
{
    using Demo.Types;

    public class Product
    {
        public Product(PositiveInt id, NonEmptyString code, NonEmptyString name)
        {
            Id = id;
            Code = code;
            Name = name;
        }

        public PositiveInt Id { get; }

        public NonEmptyString Code { get; }

        public NonEmptyString Name { get; }
    }
}
