namespace Demo.Dtos.Apis.Product.Get
{
    public class Product
    {
        public Product(int id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }

        public int Id { get; }

        public string Code { get; }

        public string Name { get; }
    }
}
