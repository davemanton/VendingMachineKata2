using System;

namespace Domain
{
    public class Product
    {
        private Product(string sku, string name)
        {
            Sku = sku;
            Name = name;
        }

        public string Sku { get; private init; }
        public string Name { get; private init; }

        public static Product CreateProduct(string sku) => sku switch
        {
            "a" => new Product("a", "cola"),
            "b" => new Product("a", "chips"),
            "c" => new Product("a", "candy"),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}