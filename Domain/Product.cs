namespace Domain
{
    public class Product
    {
        public Product(string sku, string name, decimal price)
        {
            Sku = sku;
            Name = name;
            Price = price;
        }

        public string Sku { get; private init; }
        public string Name { get; private init; }
        public decimal Price { get; private init; } 
    }
}