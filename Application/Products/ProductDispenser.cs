using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Application
{
    public class ProductDispenser : IDispenseProducts
    {
        private readonly ICollection<Product> _products;
        private readonly List<string> _dispenser;

        public ProductDispenser(ICollection<Product> products)
        {
            _dispenser = new List<string>();
            _products = products;
        }

        public void DispenseProduct(Transaction transaction, string sku)
        {
            var product = _products.Single(x => x.Sku.Equals(sku, StringComparison.OrdinalIgnoreCase));

            transaction.AddProduct(product);

            if (transaction.TryComplete())
            {
                foreach(var purchasedProduct in transaction.Products)
                    _dispenser.Add(purchasedProduct.Name);
            }
        }

        public IEnumerable<string> CheckProductHopper()
            => _dispenser;
    }
}