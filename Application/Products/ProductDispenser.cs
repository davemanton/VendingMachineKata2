using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Application
{
    public class ProductDispenser : IDispenseProducts
    {
        
        private readonly ICollection<ProductStatus> _products;
        private readonly List<string> _dispenser;

        public ProductDispenser(ICollection<ProductStatus> products)
        {
            _dispenser = new List<string>();
            _products = products;
        }

        public void DispenseProduct(Transaction transaction, string sku)
        {
            var productStatus = _products.Single(x => x.Sku.Equals(sku, StringComparison.OrdinalIgnoreCase));

            if(transaction.TryAddProduct(productStatus) && transaction.TryComplete())
            {
                foreach(var purchasedProduct in transaction.Products)
                    _dispenser.Add(purchasedProduct.Name);
            }
        }

        public IEnumerable<string> CheckProductHopper()
            => _dispenser;
    }
}