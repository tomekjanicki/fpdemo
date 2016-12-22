namespace Demo.Dtos.Apis.Product.Put
{
    public class Product
    {
        public Product(string name, int? size)
        {
            Name = name;
            Size = size;
        }

        public string Name { get; }

        public int? Size { get; }
    }
}
