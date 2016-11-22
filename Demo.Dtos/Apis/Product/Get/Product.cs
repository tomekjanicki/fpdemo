namespace Demo.Dtos.Apis.Product.Get
{
    public class Product
    {
        public Product(int id, string code, string name, int size)
        {
            Id = id;
            Code = code;
            Name = name;
            Size = size;
        }

        public int Id { get; }

        public string Code { get; }

        public string Name { get; }

        public int Size { get; }
    }
}
