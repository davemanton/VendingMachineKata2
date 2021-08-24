using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class ProductStatus
    {
        private HashSet<Product> _products;

        public ProductStatus(string sku, decimal price, int quantity)
        {
            _products = new HashSet<Product>();

            Sku = sku;
            Price = price;

            for (var i = 0; i < quantity; i++)
                _products.Add(Product.CreateProduct(sku));
        }

        public string Sku { get; private init; }
        public decimal Price { get; private init; }
        public bool IsAvailable => _products.Any();
        public IReadOnlyCollection<Product> Products => _products;

        public Product GetProduct()
        {
            var product = _products.First();
            _products.Remove(product);

            return product;
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }
    }
}