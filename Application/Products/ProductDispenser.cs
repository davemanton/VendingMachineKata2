using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Application
{
    public class ProductDispenser : IDispenseProducts
    {
        private readonly HashSet<Product> _products;
        private readonly ICollection<string> _dispenser;
        

        private ProductDispenser()
        {
            _dispenser = new List<string>();
        }

        public ProductDispenser(HashSet<Product> products)
            : this()
        {
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